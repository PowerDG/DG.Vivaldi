using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Dg.ERM.Authorization.ExtendInfos.Dtos;
using Dg.ERM.Authorization.Users;
using Dg.ERM.Roles.Dto;
using Dg.ERM.Users.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dg.ERM.Authorization.ExtendInfos.ExDtos
{
    public class RoleInfoForEditOutput
    {
        public RoleEditDto Role { get; set; }

        //public UserDto User { get; set; }



        //public List<FlatPermissionDto> Permissions { get; set; }

        //public List<string> GrantedPermissionNames { get; set; }
    }


    public class UserInfoForEditOutput
    {
        //public RoleEditDto Role { get; set; }

        public UserInfoDto User { get; set; }



        public List<ExtendInfoDto> ExtendInfos { get; set; }

        //public List<string> GrantedPermissionNames { get; set; }
    }

    [AutoMapFrom(typeof(User))]
    public class UserInfoDto : EntityDto<long>
    {
        [Required]
        [StringLength(AbpUserBase.MaxUserNameLength)]
        public string UserName { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxSurnameLength)]
        public string Surname { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }

        public bool IsActive { get; set; }

        public string FullName { get; set; }
         

        public DateTime CreationTime { get; set; }

        //public string[] RoleNames { get; set; }

        //public static implicit operator UserDto(Task<User> v)
        //{
        //    throw new NotImplementedException();
        //}

        public UserInfoDto(string userName, string name, string surName,
            string email, bool isA, string fullName, DateTime creationTime)
        {
            UserName = userName;
            Name = name;
            Surname = surName;
            EmailAddress = email;
            IsActive = isA;
            FullName = fullName;
            CreationTime = creationTime;
        }

    }
}
