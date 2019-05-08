using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dg.ERM.Contents
{
    public class DgCategory : Entity<long>, IFullAudited, IMustHaveTenant
    {
        public const int MaxStringLength = 255;
        public const int MaxContentLength = 4000;

        public long? ParentId { get; set; }

        //[ForeignKey("ParentId")]
        //public virtual Category Parent { get; set; }

        [Required]
        [MaxLength(MaxStringLength)]
        public string Title { get; set; }

        [MaxLength(MaxStringLength)]
        public string Image { get; set; }

        [MaxLength(MaxContentLength)]
        public string Content { get; set; }

        [DefaultValue(0)]
        public int SortOrder { get; set; }

        //public virtual ICollection<Category> SubCategories { get; set; }
        //public virtual ICollection<Content> Contents { get; set; }

        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public DateTime? DeletionTime { get; set; }
        public long? CreatorUserId { get; set; }
        public long? LastModifierUserId { get; set; }
        public long? DeleterUserId { get; set; }
        public bool IsDeleted { get; set; }
        public int TenantId { get; set; }

        public DgCategory()
        {
            CreationTime = Clock.Now;
            SortOrder = 0;
        }


    }
}
