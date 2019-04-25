using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DgCore.DgEnitity
{
    public class BranchInfo : Entity<int>, ICreationAudited, IDeletionAudited
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new int? Id { get; set; }


        [Required]
        public int BranchID { get; set; }

        [MaxLength(255)]
        [Required]
        public string BranchName { get; set; }
        [MaxLength(255)]
        [Required]
        public string BranchLinkMan { get; set;}
        [MaxLength(255)]
        [Required]
        public string BranchPhone { get; set; }
        [Required]
        public string BranchAddress { get; set; }
        [MaxLength(255)]
        [Required]
        public string BranchEmail { get; set; }
        public long? CreatorUserId { get ; set ; }
        public DateTime CreationTime { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
    }

}
