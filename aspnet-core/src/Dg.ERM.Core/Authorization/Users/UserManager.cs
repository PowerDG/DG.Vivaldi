using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Organizations;
using Abp.Runtime.Caching;
using Dg.ERM.Authorization.Roles;
using System.Threading.Tasks;

 
using System.Linq;
using Dg.ERM.OrganizationUnits;

namespace Dg.ERM.Authorization.Users
{
    public class UserManager : AbpUserManager<Role, User>
    {


        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;
        private readonly IRepository<UserOrganizationUnit, long> _userOrganizationUnitRepository;
        /// <summary>
        /// UserOrganizationUnitDgManagerr【人员-组织】
        /// </summary>

        protected UserOrganizationUnitDgManager<Role, User> _entityManager { get; set; }

        public UserManager(
            RoleManager roleManager,
            UserStore store, 
            IOptions<IdentityOptions> optionsAccessor, 
            IPasswordHasher<User> passwordHasher, 
            IEnumerable<IUserValidator<User>> userValidators, 
            IEnumerable<IPasswordValidator<User>> passwordValidators,
            ILookupNormalizer keyNormalizer, 
            IdentityErrorDescriber errors, 
            IServiceProvider services, 
            ILogger<UserManager<User>> logger, 
            IPermissionManager permissionManager, 
            IUnitOfWorkManager unitOfWorkManager, 
            ICacheManager cacheManager, 
            IRepository<OrganizationUnit, long> organizationUnitRepository, 
            IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository, 
            IOrganizationUnitSettings organizationUnitSettings, 
            ISettingManager settingManager)
            : base(
                roleManager, 
                store, 
                optionsAccessor, 
                passwordHasher, 
                userValidators, 
                passwordValidators, 
                keyNormalizer, 
                errors, 
                services, 
                logger, 
                permissionManager, 
                unitOfWorkManager, 
                cacheManager,
                organizationUnitRepository, 
                userOrganizationUnitRepository, 
                organizationUnitSettings, 
                settingManager)
        {

            _organizationUnitRepository = organizationUnitRepository;
            _userOrganizationUnitRepository = userOrganizationUnitRepository; 
        }

        [UnitOfWork]
        public virtual Task<List<OrganizationUnit>> GetOrganizationUnitsAsync(long userID)
        {
            var query = from uou in _userOrganizationUnitRepository.GetAll()
                        join ou in _organizationUnitRepository.GetAll() on uou.OrganizationUnitId equals ou.Id
                        where uou.UserId == userID
                        select ou;

            return Task.FromResult(query.ToList());
        }


    

        //[UnitOfWork]
        //public virtual Task<List<TUser>> GetUsersInOrganizationUnit(OrganizationUnit organizationUnit, bool includeChildren = false)
        //{
        //    if (!includeChildren)
        //    {
        //        var query = from uou in _userOrganizationUnitRepository.GetAll()
        //                    join user in AbpStore.Users on uou.UserId equals user.Id
        //                    where uou.OrganizationUnitId == organizationUnit.Id
        //                    select user;

        //        return Task.FromResult(query.ToList());
        //    }
        //    else
        //    {
        //        var query = from uou in _userOrganizationUnitRepository.GetAll()
        //                    join user in AbpStore.Users on uou.UserId equals user.Id
        //                    join ou in _organizationUnitRepository.GetAll() on uou.OrganizationUnitId equals ou.Id
        //                    where ou.Code.StartsWith(organizationUnit.Code)
        //                    select user;

        //        return Task.FromResult(query.ToList());
        //    }
        //}
    }
}
