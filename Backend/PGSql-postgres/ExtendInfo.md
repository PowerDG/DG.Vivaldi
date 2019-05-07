















```
AutoMapper.AutoMapperConfigurationException
  HResult=0x80131500
  Source=AutoMapper
  StackTrace:
   在 AutoMapper.ConfigurationValidator.AssertConfigurationIsValid(IEnumerable`1 typeMaps)
   在 AutoMapper.MapperConfiguration.AssertConfigurationIsValid(TypeMap typeMap)
   在 AutoMapper.Mapper.AutoMapper.IMapper.Map[TDestination](Object source)
   在 AutoMapper.Mapper.Map[TDestination](Object source)
   在 Dg.ERM.Users.UserAppService.SetUserInfo(ExtendInfoDto input) 在 E:\DgHub\DG.Vivaldi\aspnet-core\src\Dg.ERM.Application\Users\UserAppService.cs 中: 第 244 行
   在 Microsoft.Extensions.Internal.ObjectMethodExecutor.Execute(Object target, Object[] parameters)
   在 Microsoft.AspNetCore.Mvc.Internal.ActionMethodExecutor.SyncObjectResultExecutor.Execute(IActionResultTypeMapper mapper, ObjectMethodExecutor executor, Object controller, Object[] arguments)
   在 Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeActionMethodAsync>d__12.MoveNext()








AutoMapper.AutoMapperConfigurationException
  HResult=0x80131500
  Source=AutoMapper
  StackTrace:
   在 AutoMapper.ConfigurationValidator.AssertConfigurationIsValid(IEnumerable`1 typeMaps)
   在 AutoMapper.MapperConfiguration.AssertConfigurationIsValid(TypeMap typeMap)
   在 AutoMapper.Mapper.AutoMapper.IMapper.Map[TDestination](Object source)
   在 Abp.AutoMapper.AutoMapperObjectMapper.Map[TDestination](Object source)
   在 Dg.ERM.Users.UserAppService.SetUserInfo(ExtendInfoDto input) 在 E:\DgHub\DG.Vivaldi\aspnet-core\src\Dg.ERM.Application\Users\UserAppService.cs 中: 第 246 行
   在 Microsoft.Extensions.Internal.ObjectMethodExecutor.Execute(Object target, Object[] parameters)
   在 Microsoft.AspNetCore.Mvc.Internal.ActionMethodExecutor.SyncObjectResultExecutor.Execute(IActionResultTypeMapper mapper, ObjectMethodExecutor executor, Object controller, Object[] arguments)
   在 Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeActionMethodAsync>d__12.MoveNext()

```

















```


using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Values;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dg.ERM.Authorization.ExtendInfos
{
    //public class ExtendInfo : ValueObject, ICreationAudited, ISoftDelete
    //{
    //    public long? CreatorUserId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    //    public DateTime CreationTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    //    public bool IsDeleted { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    //    protected override IEnumerable<object> GetAtomicValues()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}


    public class ExtendInfo : Entity<long>, ICreationAudited, ISoftDelete
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new long? Id { get; set; }
        public int TenantId { get; set; }

        public long ParentId { get; set; }

        public long EnityID { get; set; }
        public string Super_Type { get; set; }
        public string EntityTypeFullName { get; set; }

        [Required]
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }
        
        public string PropertyTypeName { get; set; }
        public string Description { get; set; }
        public string DisplayName { get; set; }


        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public bool IsDeleted { get; set; }

    } 
}


```

