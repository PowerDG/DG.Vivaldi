using Abp.Authorization;
using DgERM.Authorization.Roles;
using DgERM.Authorization.Users;

namespace DgERM.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
