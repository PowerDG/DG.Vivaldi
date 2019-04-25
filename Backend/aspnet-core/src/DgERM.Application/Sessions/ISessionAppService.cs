using System.Threading.Tasks;
using Abp.Application.Services;
using DgERM.Sessions.Dto;

namespace DgERM.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
