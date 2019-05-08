using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace Dg.ERM.Contents
{ 
    public class Content : Entity<long>, IFullAudited, IMustHaveTenant
    {
        public const int MaxStringLength = 255;
        public const int MaxSummaryLength = 500;

        [Required]
        [MaxLength(MaxStringLength)]
        public string Title { get; set; }

        [Required]
        public long CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [MaxLength(MaxStringLength)]
        public string Image { get; set; }

        [MaxLength(MaxSummaryLength)]
        public string Summary { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string Body { get; set; }

        [Required]
        [DefaultValue(0)]
        public int Hits { get; set; }

        [Required]
        [DefaultValue(0)]
        public int SortOrder { get; set; }

        public virtual ICollection<ContentTag> ContentTags { get; set; }


        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public DateTime? DeletionTime { get; set; }
        public long? CreatorUserId { get; set; }
        public long? LastModifierUserId { get; set; }
        public long? DeleterUserId { get; set; }
        public bool IsDeleted { get; set; }
        public int TenantId { get; set; }

        public Content()
        {
            CreationTime = Clock.Now;
            Hits = 0;
            SortOrder = 0;

        }
    }
}
