using System.Threading.Tasks;
using Abp.Application.Services;
using Dg.ERM.Authorization.Accounts.Dto;

namespace Dg.ERM.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
