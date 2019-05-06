using Dg.ERM.Roles.Dto;
using Dg.ERM.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dg.ERM.Authorization.ExtendInfos.ExDtos
{
    public class GetRoleForEditOutput
    {
        public RoleEditDto Role { get; set; }

        public UserDto User { get; set; }



        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}
