using Abp.Domain.Repositories;
using Abp.Organizations;
using Dg.ERM.Authorization.Roles;
using Dg.ERM.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dg.ERM.OrganizationUnits
{
    

    public class OUnitDgAppService : ERMAppServiceBase
    {

        private readonly IRepository<OrganizationUnit, long> _entityRepository;

        //private readonly IProductManager _entityManager2;

        private readonly OrganizationUnitManager _entityManager;

        private readonly UserManager _userManager;

        private readonly RoleManager _roleManager;

        public OUnitDgAppService(
        IRepository<OrganizationUnit, long> entityRepository
        , OrganizationUnitManager entityManager, 
            UserManager userManager,
            RoleManager roleManager
        )
        {
            _entityRepository = entityRepository;
            _entityManager = entityManager;

            _userManager = userManager;
            _roleManager = roleManager;
        }
        #region MyRegion


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
         
        public virtual async Task GetUsersInOrganizationUnitAsync(long ouId, bool includeChildren = false)
        {
            var organizationUnit = _entityRepository.FirstOrDefault(ouId);
            await _userManager.GetUsersInOrganizationUnit(organizationUnit, includeChildren);
        }
        #endregion


    }
}
