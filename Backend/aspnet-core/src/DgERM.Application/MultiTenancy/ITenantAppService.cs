using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DgERM.MultiTenancy.Dto;

namespace DgERM.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

