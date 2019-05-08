using Abp.Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dg.ERM.Contents
{
    //[Table("AppTags")]
    public class Tag : Entity<long>, IMustHaveTenant
    {
        public const int MaxNameLength = 50;

        [Required]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; }

        public int TenantId { get; set; }

        public virtual ICollection<ContentTag> ContentTags { get; set; }

    }
}