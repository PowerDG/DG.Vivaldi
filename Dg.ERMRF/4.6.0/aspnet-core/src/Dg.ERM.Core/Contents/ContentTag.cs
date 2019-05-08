using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Dg.ERM.Contents
{
    //[Table("AppContentTags")]
    public class ContentTag : Entity<long>
    {
        [Required]
        public long ContentId { get; set; }
        //[ForeignKey("ContentId")]
        public virtual Content Content { get; set; }

        [Required]
        public long TagId { get; set; }
        //[ForeignKey("TagId")]
        public virtual Tag Tag { get; set; }

    }
}