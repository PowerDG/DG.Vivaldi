using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Abp.Application.Features;
using Abp.Authorization.Roles;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.IdentityFramework;
using Abp.Localization;
using Abp.MultiTenancy;
using Abp.Organizations;
using Abp.Runtime.Caching;
using Abp.Runtime.Security;
using Abp.Runtime.Session;
using Abp.Zero;
using Abp.Zero.Configuration;
//using Microsoft.AspNet.Identity;
using Abp.Authorization;
using Abp.Authorization.Users;
using Dg.ERM.Authorization.Users;
using Dg.ERM.Authorization.Roles;
using Abp;

namespace Dg.ERM.Authorization.OrganizationUnits
{
    public class UserOrganizationUnitManager<TRole, TUser> : 
        IDomainService
        where TRole : AbpRole<TUser>, new()
        where TUser : AbpUser<TUser>
    {


        private readonly UserManager _userManager;
        public ILocalizationManager LocalizationManager { get; }

        protected string LocalizationSourceName { get; set; }

        protected AbpRoleManager<TRole, TUser> RoleManager { get; }

        public AbpUserStore<TRole, TUser> AbpStore { get; }

        public IAbpSession AbpSession { get; set; }

        public FeatureDependencyContext FeatureDependencyContext { get; set; }
        public IMultiTenancyConfig MultiTenancy { get; set; }

        private readonly IPermissionManager _permissionManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly ICacheManager _cacheManager;
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;
        private readonly IRepository<UserOrganizationUnit, long> _userOrganizationUnitRepository;
        private readonly IOrganizationUnitSettings _organizationUnitSettings;
        private readonly ISettingManager _settingManager;
        protected UserOrganizationUnitManager(
           AbpUserStore<TRole, TUser> userStore,
           AbpRoleManager<TRole, TUser> roleManager,
           UserManager userManager,

           IPermissionManager permissionManager,
           IUnitOfWorkManager unitOfWorkManager,
           ICacheManager cacheManager,
           IRepository<OrganizationUnit, long> organizationUnitRepository,
           IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository,
           IOrganizationUnitSettings organizationUnitSettings,
           ILocalizationManager localizationManager,
           //IdentityEmailMessageService emailService,
           ISettingManager settingManager
            //IUserTokenProviderAccessor userTokenProviderAccessor
            )
           //: base(userStore)
        {
            AbpStore = userStore;
            RoleManager = roleManager;
            _userManager = userManager;
            LocalizationManager = localizationManager;
            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
            _settingManager = settingManager;

            _permissionManager = permissionManager;
            _unitOfWorkManager = unitOfWorkManager;
            _cacheManager = cacheManager;
            _organizationUnitRepository = organizationUnitRepository;
            _userOrganizationUnitRepository = userOrganizationUnitRepository;
            _organizationUnitSettings = organizationUnitSettings;

            AbpSession = NullAbpSession.Instance;

            //UserLockoutEnabledByDefault = true;
            //DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            //MaxFailedAccessAttemptsBeforeLockout = 5;

            //EmailService = emailService;

            //UserTokenProvider = userTokenProviderAccessor.GetUserTokenProviderOrNull<TUser>();
        }

        #region UserBase

        /// <summary>
        /// Gets a user by given id.
        /// Throws exception if no user found with given id.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>User</returns>
        /// <exception cref="AbpException">Throws exception if no user found with given id</exception>
        //public virtual async Task<TUser> GetUserByIdAsync2(long userId)
        //{
        //    var user = await _userManager.FindByIdAsync(userId.ToString());
        //    if (user == null)
        //    {
        //        throw new AbpException("There is no user with id: " + userId);
        //    }
        //    return user;
        //}

        public virtual async Task<User> GetUserByIdAsync(long userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new AbpException("There is no user with id: " + userId);
            }
            return user;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ouId"></param>
        /// <returns></returns>
        /// 



        public virtual async Task<bool> IsInOrganizationUnitAsync(long userId, long ouId)
        {
            return await IsInOrganizationUnitAsync(
               (await GetUserByIdAsync(userId)).Id,
               (await _organizationUnitRepository.GetAsync(ouId)).Id
                );
        }

        public virtual async Task<bool> IsInOrganizationUnitAsync(TUser user, OrganizationUnit ou)
        {
            return await _userOrganizationUnitRepository.CountAsync(uou =>
                uou.UserId == user.Id && uou.OrganizationUnitId == ou.Id
                ) > 0;
        }

        public virtual async Task AddToOrganizationUnitAsync(long userId, long ouId)
        {
            await AddToOrganizationUnitAsync(
               (await GetUserByIdAsync(userId)).Id,
               (await _organizationUnitRepository.GetAsync(ouId)).Id
                );
        }

        public virtual async Task AddToOrganizationUnitAsync(TUser user, OrganizationUnit ou)
        {
            var currentOus = await GetOrganizationUnitsAsync(user);

            if (currentOus.Any(cou => cou.Id == ou.Id))
            {
                return;
            }

            await CheckMaxUserOrganizationUnitMembershipCountAsync(user.TenantId, currentOus.Count + 1);

            await _userOrganizationUnitRepository.InsertAsync(new UserOrganizationUnit(user.TenantId, user.Id, ou.Id));
        }

        public virtual async Task RemoveFromOrganizationUnitAsync(long userId, long ouId)
        {
            await RemoveFromOrganizationUnitAsync(
               (await GetUserByIdAsync(userId)).Id,
               (await _organizationUnitRepository.GetAsync(ouId)).Id
                );
        }

        public virtual async Task RemoveFromOrganizationUnitAsync(TUser user, OrganizationUnit ou)
        {
            await _userOrganizationUnitRepository.DeleteAsync(uou => uou.UserId == user.Id && uou.OrganizationUnitId == ou.Id);
        }

        public virtual async Task SetOrganizationUnitsAsync(long userId, params long[] organizationUnitIds)
        {
            await SetOrganizationUnitsAsync(
                (await GetUserByIdAsync(userId)).Id,
                organizationUnitIds
                );
        }

        private async Task CheckMaxUserOrganizationUnitMembershipCountAsync(int? tenantId, int requestedCount)
        {
            var maxCount = await _organizationUnitSettings.GetMaxUserMembershipCountAsync(tenantId);
            if (requestedCount > maxCount)
            {
                throw new AbpException(string.Format("Can not set more than {0} organization unit for a user!", maxCount));
            }
        }

        public virtual async Task SetOrganizationUnitsAsync(TUser user, params long[] organizationUnitIds)
        {
            if (organizationUnitIds == null)
            {
                organizationUnitIds = new long[0];
            }

            await CheckMaxUserOrganizationUnitMembershipCountAsync(user.TenantId, organizationUnitIds.Length);

            var currentOus = await GetOrganizationUnitsAsync(user);

            //Remove from removed OUs
            foreach (var currentOu in currentOus)
            {
                if (!organizationUnitIds.Contains(currentOu.Id))
                {
                    await RemoveFromOrganizationUnitAsync(user, currentOu);
                }
            }

            //Add to added OUs
            foreach (var organizationUnitId in organizationUnitIds)
            {
                if (currentOus.All(ou => ou.Id != organizationUnitId))
                {
                    await AddToOrganizationUnitAsync(
                        user,
                        await _organizationUnitRepository.GetAsync(organizationUnitId)
                        );
                }
            }
        }

        [UnitOfWork]
        public virtual Task<List<OrganizationUnit>> GetOrganizationUnitsAsync(TUser user)
        {
            var query = from uou in _userOrganizationUnitRepository.GetAll()
                        join ou in _organizationUnitRepository.GetAll() on uou.OrganizationUnitId equals ou.Id
                        where uou.UserId == user.Id
                        select ou;

            return Task.FromResult(query.ToList());
        }

        [UnitOfWork]
        public virtual Task<List<TUser>> GetUsersInOrganizationUnit(OrganizationUnit organizationUnit, bool includeChildren = false)
        {
            if (!includeChildren)
            {
                var query = from uou in _userOrganizationUnitRepository.GetAll()
                            join user in AbpStore.Users on uou.UserId equals user.Id
                            where uou.OrganizationUnitId == organizationUnit.Id
                            select user;

                return Task.FromResult(query.ToList());
            }
            else
            {
                var query = from uou in _userOrganizationUnitRepository.GetAll()
                            join user in AbpStore.Users on uou.UserId equals user.Id
                            join ou in _organizationUnitRepository.GetAll() on uou.OrganizationUnitId equals ou.Id
                            where ou.Code.StartsWith(organizationUnit.Code)
                            select user;

                return Task.FromResult(query.ToList());
            }
        }

    }
}
