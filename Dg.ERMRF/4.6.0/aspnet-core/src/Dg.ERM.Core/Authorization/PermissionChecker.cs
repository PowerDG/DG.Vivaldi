using Abp.Authorization;
using Dg.ERM.Authorization.Roles;
using Dg.ERM.Authorization.Users;

namespace Dg.ERM.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
