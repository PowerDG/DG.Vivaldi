using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Dg.ERM.MultiTenancy.Dto;

namespace Dg.ERM.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

