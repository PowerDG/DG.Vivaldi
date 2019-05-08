using System.Threading.Tasks;
using Abp.Application.Services;
using Dg.ERM.Sessions.Dto;

namespace Dg.ERM.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
