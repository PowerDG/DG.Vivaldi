using System.Threading.Tasks;
using Abp.Application.Services;
using DgERM.Authorization.Accounts.Dto;

namespace DgERM.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
