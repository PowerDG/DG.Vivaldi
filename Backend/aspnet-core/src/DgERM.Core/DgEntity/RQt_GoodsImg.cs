using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DgCore.DgEnitity
{
    public class T_GoodsImg : Entity<int>, ICreationAudited, IDeletionAudited, IModificationAudited
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new int? Id { get; set; }


        public long TGID { get; set; }

        [MaxLength(255)]
        public string ImgUrl { get; set; }
        [MaxLength(50)]
        public string Remark { get; set; }

        public DateTime AddTime { get; set; }

        public long? CreatorUserId { get;set; }
        public DateTime CreationTime { get;set; }
        public long? DeleterUserId { get;set; }
        public DateTime? DeletionTime { get;set; }
        public bool IsDeleted { get;set; }
        public long? LastModifierUserId { get;set; }
        public DateTime? LastModificationTime { get;set; }
    }
}
