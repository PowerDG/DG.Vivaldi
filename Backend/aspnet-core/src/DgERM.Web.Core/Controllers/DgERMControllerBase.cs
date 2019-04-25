using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace DgERM.Controllers
{
    public abstract class DgERMControllerBase: AbpController
    {
        protected DgERMControllerBase()
        {
            LocalizationSourceName = DgERMConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
