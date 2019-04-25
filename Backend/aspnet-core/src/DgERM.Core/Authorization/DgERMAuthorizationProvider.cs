using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace DgERM.Authorization
{
    public class DgERMAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);

            var pages = context.GetPermissionOrNull(PermissionNames.Pages);
            if (pages == null)
                pages = context.CreatePermission(PermissionNames.Pages, L("Pages"));
            //Tasks
            var tasks = pages.CreateChildPermission(PermissionNames.Pages_Tasks, L("Tasks"));
            tasks.CreateChildPermission(PermissionNames.Pages_Tasks_AssignPerson, L("AssignTaskToPerson"));
            tasks.CreateChildPermission(PermissionNames.Pages_Tasks_Delete, L("DeleteTask"));

        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, DgERMConsts.LocalizationSourceName);
        }
    }
}
