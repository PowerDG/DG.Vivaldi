using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace Dg.ERM.Controllers
{
    public abstract class ERMControllerBase: AbpController
    {
        protected ERMControllerBase()
        {
            LocalizationSourceName = ERMConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
