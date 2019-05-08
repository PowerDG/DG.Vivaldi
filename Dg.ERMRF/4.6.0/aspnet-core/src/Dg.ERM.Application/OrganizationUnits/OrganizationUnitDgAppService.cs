using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Abp.UI;
using Abp.Extensions;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Application.Services.Dto;
using Abp.Linq.Extensions;
using Abp.AutoMapper;
using Abp.Domain.Uow;
using DG.ERM.Products;
using DG.ERM.Products.DomainService;
using Abp.Organizations;
using Dg.ERM.Authorization.OrganizationUnits;
using Abp.Authorization.Users;
using Abp.Authorization.Roles;

namespace Dg.ERM.OrganizationUnits
{

     
        public class OrganizationUnitDgAppService<TRole, TUser> : ERMAppServiceBase, IOrganizationUnitDgAppService
        where TRole : AbpRole<TUser>, new()
        where TUser : AbpUser<TUser>
    {

        private readonly IRepository<OrganizationUnit, long> _entityRepository;

        private readonly IProductManager _entityManager2;

        private readonly OrganizationUnitManager _organizationUnitManager;

        protected UserOrganizationUnitDgManager<TRole, TUser> _entityManager { get; set; } 
        //private readonly UserOrganizationUnitManager _entityManager;

        public OrganizationUnitDgAppService(
        IRepository<OrganizationUnit, long> entityRepository
        , OrganizationUnitManager organizationUnitManager
            , UserOrganizationUnitDgManager<TRole, TUser> entityManager
        )
        {
            _entityRepository = entityRepository;
            _organizationUnitManager = organizationUnitManager;
            _entityManager = entityManager;
        }


        #region UserOrganizationUnitManager 组织与用户

        public virtual async Task<bool> IsInOrganizationUnitAsync(long userId, long ouId)
        {
            return await _entityManager.IsInOrganizationUnitAsync
             (userId, ouId);
        }

        public async Task AddToOrganizationUnitAsync(long userId, long ouId)
        {
            await _entityManager.AddToOrganizationUnitAsync(userId, ouId);
        }

        public virtual async Task RemoveFromOrganizationUnitAsync(long userId, long ouId)
        {
            await _entityManager.RemoveFromOrganizationUnitAsync(userId, ouId);
        }
        public virtual async Task SetOrganizationUnitsAsync(long userId, params long[] organizationUnitIds)
        {
            await _entityManager.SetOrganizationUnitsAsync(userId, organizationUnitIds);
        }
        public virtual Task<List<OrganizationUnit>> GetOrganizationUnitsAsync2(long userID)
        { 
            return _entityManager.GetOrganizationUnitsAsync2(userID);
        }
        public virtual Task<List<OrganizationUnit>> GetOrganizationUnitsAsync(TUser user)
        {
             return _entityManager.GetOrganizationUnitsAsync(user); 
        }

        public virtual async Task GetUsersInOrganizationUnitAsync(long ouId, bool includeChildren = false) 
        {
            await _entityManager.GetUsersInOrganizationUnitAsync(ouId, includeChildren); 
        }

        #endregion



    }
}
