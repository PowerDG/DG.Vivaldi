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

