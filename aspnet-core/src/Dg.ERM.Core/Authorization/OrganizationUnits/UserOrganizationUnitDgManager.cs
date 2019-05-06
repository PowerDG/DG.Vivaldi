using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.Localization;
using Abp.Organizations;
using Abp.Runtime.Caching;
using Abp.Zero;
using Dg.ERM.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dg.ERM.OrganizationUnits
{
    public class UserOrganizationUnitDgManager<TRole, TUser> :  
        IDomainService
        where TRole : AbpRole<TUser>, new()
        where TUser : AbpUser<TUser>
    {
        private readonly UserManager _userManager;


        public ILocalizationManager LocalizationManager { get; }

        public IMultiTenancyConfig MultiTenancy { get; set; }


        private readonly IPermissionManager _permissionManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        protected string LocalizationSourceName { get; set; }
        private readonly ICacheManager _cacheManager;
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;
        private readonly IRepository<UserOrganizationUnit, long> _userOrganizationUnitRepository;
        private readonly IOrganizationUnitSettings _organizationUnitSettings;
        private readonly ISettingManager _settingManager;

        protected UserOrganizationUnitDgManager(

           UserManager userManager,

           IRepository<OrganizationUnit, long> organizationUnitRepository,
           IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository,

           IPermissionManager permissionManager,
           IUnitOfWorkManager unitOfWorkManager,
           ICacheManager cacheManager,

           IOrganizationUnitSettings organizationUnitSettings,
           ILocalizationManager localizationManager,
           //IdentityEmailMessageService emailService,
           ISettingManager settingManager
            )
        {
            _userManager = userManager;
            LocalizationManager = localizationManager;
            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
            _settingManager = settingManager;

            _unitOfWorkManager = unitOfWorkManager;
            _cacheManager = cacheManager;
            _organizationUnitRepository = organizationUnitRepository;
            _userOrganizationUnitRepository = userOrganizationUnitRepository;
            _organizationUnitSettings = organizationUnitSettings;

            //AbpSession = NullAbpSession.Instance;
        }


        public virtual async Task<bool> IsInOrganizationUnitAsync(long userId, long ouId)
        {
            return await _userManager.IsInOrganizationUnitAsync
             (userId, ouId);
        }
        public async Task AddToOrganizationUnitAsync(long userId, long ouId)
        {
            await _userManager.AddToOrganizationUnitAsync(userId, ouId);
        }

        public virtual async Task RemoveFromOrganizationUnitAsync(long userId, long ouId)
        { 
            await _userManager.RemoveFromOrganizationUnitAsync(userId, ouId); 
        }

        public virtual async Task SetOrganizationUnitsAsync(long userId, params long[] organizationUnitIds)
        { 
            await _userManager.SetOrganizationUnitsAsync(userId, organizationUnitIds); 
        }

        [UnitOfWork]
        public virtual Task<List<OrganizationUnit>> GetOrganizationUnitsAsync2(long userID)
        {
            var query = from uou in _userOrganizationUnitRepository.GetAll()
                        join ou in _organizationUnitRepository.GetAll() on uou.OrganizationUnitId equals ou.Id
                        where uou.UserId == userID
                        select ou;

            return Task.FromResult(query.ToList());
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

        public virtual async Task  GetUsersInOrganizationUnitAsync(long ouId, bool includeChildren = false)

        {
            //var organizationUnit2 = new OrganizationUnit(); 
            var organizationUnit= _organizationUnitRepository.FirstOrDefault(t => t.Id == ouId); 
            await _userManager.GetUsersInOrganizationUnit(organizationUnit, includeChildren); 
        }


    }
}
