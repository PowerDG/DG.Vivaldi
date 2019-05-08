using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using Abp.Localization;
using Abp.Organizations;
using Abp.Runtime.Session;
using Abp.UI;
using AutoMapper;
using Dg.ERM.Authorization;
using Dg.ERM.Authorization.Accounts;
using Dg.ERM.Authorization.ExtendInfos;
using Dg.ERM.Authorization.ExtendInfos.DomainService;
using Dg.ERM.Authorization.ExtendInfos.Dtos;
using Dg.ERM.Authorization.ExtendInfos.ExDtos;
using Dg.ERM.Authorization.Roles;
using Dg.ERM.Authorization.Users;
using Dg.ERM.OrganizationUnits;
using Dg.ERM.Roles.Dto;
using Dg.ERM.Users.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Dg.ERM.Users
{
    [AbpAuthorize(PermissionNames.Pages_Users)]
    public class UserAppService : AsyncCrudAppService<User, UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>, IUserAppService
    {
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IRepository<Role> _roleRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IAbpSession _abpSession;
        private readonly LogInManager _logInManager;
        private readonly ExtendInfoManager _extendInfoManager;


        //protected UserOrganizationUnitDgManager<TRole, TUser> _OUDgManager { get; set; }
        IRepository<ExtendInfo, long> _extendRepository;

        public UserAppService(
            IRepository<User, long> repository,
            UserManager userManager,
            RoleManager roleManager,
            IRepository<Role> roleRepository,
            IPasswordHasher<User> passwordHasher,
            IAbpSession abpSession,
            LogInManager logInManager,

        IRepository<ExtendInfo, long> extendRepository,
        ExtendInfoManager extendInfoManager)
            : base(repository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _roleRepository = roleRepository;
            _passwordHasher = passwordHasher;
            _abpSession = abpSession;
            _logInManager = logInManager;
            _extendInfoManager = extendInfoManager;
            _extendRepository=extendRepository;
        }

        public override async Task<UserDto> Create(CreateUserDto input)
        {
            CheckCreatePermission();

            var user = ObjectMapper.Map<User>(input);

            user.TenantId = AbpSession.TenantId;
            user.IsEmailConfirmed = true;

            await _userManager.InitializeOptionsAsync(AbpSession.TenantId);

            CheckErrors(await _userManager.CreateAsync(user, input.Password));

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRoles(user, input.RoleNames));
            }

            CurrentUnitOfWork.SaveChanges();

            return MapToEntityDto(user);
        }

        public override async Task<UserDto> Update(UserDto input)
        {
            CheckUpdatePermission();

            var user = await _userManager.GetUserByIdAsync(input.Id);

            MapToEntity(input, user);

            CheckErrors(await _userManager.UpdateAsync(user));

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRoles(user, input.RoleNames));
            }

            return await Get(input);
        }

        public override async Task Delete(EntityDto<long> input)
        {
            var user = await _userManager.GetUserByIdAsync(input.Id);
            await _userManager.DeleteAsync(user);
        }

        public async Task<ListResultDto<RoleDto>> GetRoles()
        {
            var roles = await _roleRepository.GetAllListAsync();
            return new ListResultDto<RoleDto>(ObjectMapper.Map<List<RoleDto>>(roles));
        }

        public async Task ChangeLanguage(ChangeUserLanguageDto input)
        {
            await SettingManager.ChangeSettingForUserAsync(
                AbpSession.ToUserIdentifier(),
                LocalizationSettingNames.DefaultLanguage,
                input.LanguageName
            );
        }

        protected override User MapToEntity(CreateUserDto createInput)
        {
            var user = ObjectMapper.Map<User>(createInput);
            user.SetNormalizedNames();
            return user;
        }

        protected override void MapToEntity(UserDto input, User user)
        {
            ObjectMapper.Map(input, user);
            user.SetNormalizedNames();
        }

        protected override UserDto MapToEntityDto(User user)
        {
            var roles = _roleManager.Roles.Where(r => user.Roles.Any(ur => ur.RoleId == r.Id)).Select(r => r.NormalizedName);
            var userDto = base.MapToEntityDto(user);
            userDto.RoleNames = roles.ToArray();
            return userDto;
        }

        protected override IQueryable<User> CreateFilteredQuery(PagedUserResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.Roles)
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.UserName.Contains(input.Keyword) || x.Name.Contains(input.Keyword) || x.EmailAddress.Contains(input.Keyword))
                .WhereIf(input.IsActive.HasValue, x => x.IsActive == input.IsActive);
        }

        protected override async Task<User> GetEntityByIdAsync(long id)
        {
            var user = await Repository.GetAllIncluding(x => x.Roles).FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                throw new EntityNotFoundException(typeof(User), id);
            }

            return user;
        }

        protected override IQueryable<User> ApplySorting(IQueryable<User> query, PagedUserResultRequestDto input)
        {
            return query.OrderBy(r => r.UserName);
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        public async Task<bool> ChangePassword(ChangePasswordDto input)
        {
            if (_abpSession.UserId == null)
            {
                throw new UserFriendlyException("Please log in before attemping to change password.");
            }
            long userId = _abpSession.UserId.Value;
            var user = await _userManager.GetUserByIdAsync(userId);
            var loginAsync = await _logInManager.LoginAsync(user.UserName, input.CurrentPassword, shouldLockout: false);
            if (loginAsync.Result != AbpLoginResultType.Success)
            {
                throw new UserFriendlyException("Your 'Existing Password' did not match the one on record.  Please try again or contact an administrator for assistance in resetting your password.");
            }
            if (!new Regex(AccountAppService.PasswordRegex).IsMatch(input.NewPassword))
            {
                throw new UserFriendlyException("Passwords must be at least 8 characters, contain a lowercase, uppercase, and number.");
            }
            user.Password = _passwordHasher.HashPassword(user, input.NewPassword);
            CurrentUnitOfWork.SaveChanges();
            return true;
        }

        public async Task<bool> ResetPassword(ResetPasswordDto input)
        {
            if (_abpSession.UserId == null)
            {
                throw new UserFriendlyException("Please log in before attemping to reset password.");
            }
            long currentUserId = _abpSession.UserId.Value;
            var currentUser = await _userManager.GetUserByIdAsync(currentUserId);
            var loginAsync = await _logInManager.LoginAsync(currentUser.UserName, input.AdminPassword, shouldLockout: false);
            if (loginAsync.Result != AbpLoginResultType.Success)
            {
                throw new UserFriendlyException("Your 'Admin Password' did not match the one on record.  Please try again.");
            }
            if (currentUser.IsDeleted || !currentUser.IsActive)
            {
                return false;
            }
            var roles = await _userManager.GetRolesAsync(currentUser);
            if (!roles.Contains(StaticRoleNames.Tenants.Admin))
            {
                throw new UserFriendlyException("Only administrators may reset passwords.");
            }

            var user = await _userManager.GetUserByIdAsync(input.UserId);
            if (user != null)
            {
                user.Password = _passwordHasher.HashPassword(user, input.NewPassword);
                CurrentUnitOfWork.SaveChanges();
            }

            return true;
        }
        string  EntityTypeName = "TUser";





        [UnitOfWork]
        public List<ExtendInfoDto> SetUserInfo2(ExtendInfoEditDto input)
        {
            var entity = Mapper.Map<ExtendInfo>(input);
            var userID = entity.EnityID;
             
            //var extendInfo = _extendRepository.BindToInfo(entity, EntityTypeName, userID); 
            entity.EntityTypeFullName = EntityTypeName;
            entity.Id = null;
            if (entity != null)
            {
                //_entityRepository.Insert(entity);

                var extendInfo = _extendRepository.InsertAndGetId(entity);

                CurrentUnitOfWork.SaveChanges();

                var extendInfos = _extendInfoManager.GetAllInfo() 
                    .Where(t => t.EnityID == userID && 
                    t.EntityTypeFullName == EntityTypeName);

                return Mapper.Map<List<ExtendInfoDto>>(extendInfos).ToList();
                //return AutoMapper.Mapper.Map<ExtendInfoEditDto>(input);
            }
            return null;
        }




        public   async Task<UserDto> Update2(UserDto input)
        {
            CheckUpdatePermission();

            var user = await _userManager.GetUserByIdAsync(input.Id);

            MapToEntity(input, user);

            CheckErrors(await _userManager.UpdateAsync(user));

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRoles(user, input.RoleNames));
            }

            return await Get(input);
        }


        [UnitOfWork]
        public async Task<UserInfoForEditOutput> SetUserInfo3(ExtendInfoEditDto input)
        {


            var user6 = await _userManager.GetUserByIdAsync(input.EnityID);

            //MapToEntity(input, user);

            //CheckErrors(await _userManager.UpdateAsync(user));

            //if (input.RoleNames != null)
            //{
            //    CheckErrors(await _userManager.SetRoles(user, input.RoleNames));
            //} 
            var entity = Mapper.Map<ExtendInfo>(input);
            var userID = entity.EnityID; 
            #region MyRegion 
            //var extendInfo = _extendRepository.BindToInfo(entity, EntityTypeName, userID); 
            entity.EntityTypeFullName = EntityTypeName;
            entity.Id = null;
            if (entity != null)
            {  
                var extendInfo = _extendInfoManager.InsertToInfo(entity); 
                CurrentUnitOfWork.SaveChanges(); 
            }
            #endregion
            var user = _userManager.GetUserByIdAsync(userID);
            var extendInfos = _extendRepository.GetAll()
                .Where(t => t.EnityID == userID &&
                t.EntityTypeFullName == EntityTypeName);


            var user2 = Repository.GetAllIncluding(x => x.Roles).FirstOrDefaultAsync(x => x.Id == userID);


            //var userEditDto = ObjectMapper.Map<UserInfoDto>(user);
            return  new UserInfoForEditOutput
            {
                //User = Mapper.Map<UserDto>(user),
                User = Mapper.Map<UserInfoDto>(user2),
                ExtendInfos = Mapper.Map<List<ExtendInfoDto>>(extendInfos).ToList()
            };
            //return Task<UserInfoForEditOutput>(a);
        }


        #region Map User for organizationUnitIds
        /// <summary>
        /// user转Dto映射
        /// 犹豫AutoMapper尚未解决的零时方案
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public UserInfoDto toInfo(User user)
        {
            return new UserInfoDto(
                //User.user,

                user.UserName,
                user.Name,
                user.Surname,
                user.EmailAddress,
                user.IsActive,
                user.FullName,
                user.CreationTime
                );
        }


        #endregion



        /// <summary>
        /// 为用户添加扩展属性
        /// 并获取用户信息【以及扩展属性】
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public UserInfoForEditOutput SetUserInfo(ExtendInfoEditDto input )
        {

        
            var entity = Mapper.Map<ExtendInfo>(input); 
            var userID = entity.EnityID; 
            #region 添加Info表信息 
            //var extendInfo = _extendRepository.BindToInfo(entity, EntityTypeName, userID); 
            entity.EntityTypeFullName = EntityTypeName; 
            entity.Id = null;
            if (entity != null)
            {
                //_entityRepository.Insert(entity); 
                var extendInfo = _extendInfoManager.InsertToInfo(entity);
               var indexInfo = extendInfo.Id;
                CurrentUnitOfWork.SaveChanges();
                //return AutoMapper.Mapper.Map<ExtendInfoEditDto>(input);
            } 
            #endregion  
            //var user = _userManager.GetUserByIdAsync(userID); 
            var extendInfos = _extendRepository.GetAll() 
                .Where(t => t.EnityID == userID && 
                t.EntityTypeFullName == EntityTypeName);
             
            var user =   Repository.GetAllIncluding(x => x.Roles).FirstOrDefault(x => x.Id == userID); 
            //var userEditDto = ObjectMapper.Map<UserInfoDto>(user);
            return   new UserInfoForEditOutput
            {  //User = Mapper.Map<UserDto>(user),
                User= toInfo(user),
                ExtendInfos = Mapper.Map<List<ExtendInfoDto>>(extendInfos).ToList()
            };
        }

        /// <summary> 
        /// 并获取用户信息【以及扩展属性】
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public UserInfoForEditOutput GetUserInfo(long userID)
        {
            //var EntityTypeName = "TUser";
            var extendInfos = _extendInfoManager.GetAllInfo()
                .Where(t => t.EnityID == userID &&   
                t.EntityTypeFullName==EntityTypeName);

            var user = Repository.GetAllIncluding(x => x.Roles).FirstOrDefault(x => x.Id == userID);
            return new UserInfoForEditOutput
            {
                //User = Mapper.Map<UserDto>(user),
                User = toInfo(user),
                ExtendInfos =Mapper.Map<List<ExtendInfoDto>>(extendInfos).ToList()
                //ExtendInfos = mextendInfos;

                //Role = roleEditDto,
                //Permissions = ObjectMapper.Map<List<FlatPermissionDto>>(permissions).OrderBy(p => p.DisplayName).ToList(),
                //GrantedPermissionNames = grantedPermissions.Select(p => p.Name).ToList()

            };
        }


        //public AddToOrganizationUnitAsync
        public virtual async Task AddToOrganizationUnitAsync(long userId, long ouId)
        {
            await _userManager.AddToOrganizationUnitAsync(userId, ouId);
        }

        public virtual async Task RemoveFromOrganizationUnitAsync(long userId, long ouId)
        {
            await _userManager.RemoveFromOrganizationUnitAsync(userId, ouId);
        }

        /// <summary>
        /// 用户号获取组织信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public virtual async Task<List<OrganizationUnit>> GetOrganizationUnitsAsync(long userID)
        {

            return await _userManager.GetOrganizationUnitsAsync(userID);

        }
       
        
        /// <summary>
        /// 组织号获取用户信息
        /// </summary>
        /// <param name="organizationUnit"></param>
        /// <param name="includeChildren"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual Task<List<User>> GetUsersInOrganizationUnit(OrganizationUnit organizationUnit, bool includeChildren = false)
        {
            var users = _userManager.GetUsersInOrganizationUnit(organizationUnit, includeChildren);
            return users;

        }

    }
}

