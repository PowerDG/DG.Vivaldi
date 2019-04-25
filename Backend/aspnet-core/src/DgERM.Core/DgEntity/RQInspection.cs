using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DgCore.DgEnitity
{/// <summary>
/// 审核表
/// </summary>
   public class Inspection:Entity<long>,IFullAudited
    {
      

        public new int ? Id { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Inspection_No { get; set; }
        public string SourceType { get; set; }
        public long SourceNo { get; set; }
        public DateTime StartDate { get; set; }
        public string DestinationNO { get; set; }
        public int Version { get; set; }
        public string ProposerName { get; set; }
        public long ProposerID { get; set; }
        [MaxLength(100)]


        public string Sorting { get; set; }
        public string Title { get; set; }
        public bool IsCandidate { get; set; }
        public int InspectionState { get; set; }
        [MaxLength(100)]
        public string InspectionName { get; set; }
        public string Action { get; set; }
        [MaxLength(255)]
        public string InspectionMemo { get; set; }
        public DateTime? EndDate { get; set; }
        public int Quality_level  { get; set; }


        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }

    }
}
