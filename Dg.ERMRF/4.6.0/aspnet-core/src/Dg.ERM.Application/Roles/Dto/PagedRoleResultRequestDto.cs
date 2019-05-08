using Abp.Application.Services.Dto;

namespace Dg.ERM.Roles.Dto
{
    public class PagedRoleResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

