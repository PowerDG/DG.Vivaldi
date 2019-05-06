using Abp.Application.Services;
using Abp.Organizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dg.ERM.OrganizationUnits
{
    public interface IOrganizationUnitDgAppService : IApplicationService
    {
        Task<bool> IsInOrganizationUnitAsync(long userId, long ouId);
        Task AddToOrganizationUnitAsync(long userId, long ouId);
        Task RemoveFromOrganizationUnitAsync(long userId, long ouId);
        Task SetOrganizationUnitsAsync(long userId, params long[] organizationUnitIds);

        Task<List<OrganizationUnit>> GetOrganizationUnitsAsync2(long userID);

        Task GetUsersInOrganizationUnitAsync(long ouId, bool includeChildren = false);


    }
}
