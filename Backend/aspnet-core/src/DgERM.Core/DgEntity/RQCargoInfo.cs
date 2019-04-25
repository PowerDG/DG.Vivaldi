
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DgCore.DgEnitity
{
    public class CargoInfo : Entity<int>, ICreationAudited, IDeletionAudited,IModificationAudited
    {


     

        [Key]
        public long CargoID { get; set; }

        public new long Id { get; set; }

        [MaxLength(255)]
        [Required]
        public string CargoName { get; set; }
        [MaxLength(255)]
        [Required]
        public string CargoWeight { get; set; }
        public decimal Dimensions_L { get; set; }
        public decimal Dimensions_W { get; set; }
        public decimal Dimensions_H { get; set;}
        [MaxLength(255)]
        [Required]
        public string CargoBulk { get; set; }
        [MaxLength(255)]
        [Required]
        public string CargoNum { get; set; }
        [MaxLength(255)]
        public string CargoUnit { get; set; }
        [MaxLength(255)]
        public string CargoValue { get; set; }
        [MaxLength(255)]
        public string CargoFreight { get; set; }
        [MaxLength(255)]
        public string CargoAmends { get; set; }
        [MaxLength(255)]
        public string CargoMemo { get; set; }
        [DefaultValue(0)]
        public int CargoState { get; set; }
        [MaxLength(255)]
        public int BranchID { get; set; }
        public DateTime? CargoStartData { get; set; }
        
        public DateTime? CargoEndData { get; set; }
       
        public long? CreatorUserId { get;set; }
        public DateTime CreationTime { get;set; }
        public long? DeleterUserId { get;set; }
        public DateTime? DeletionTime { get;set; }
        public bool IsDeleted { get;set; }
        public long? LastModifierUserId { get;set; }
        public DateTime? LastModificationTime { get;set; }
    }
    public class CargoVector : Entity, ICreationAudited, IDeletionAudited, IModificationAudited
    {   [Key]

        public int CVID { get; set; }

        [Required]
        public  int CargoID { get; set; }
        [Required]
        public int BillID { get; set;}

        public long? CreatorUserId { get;set; }
        public DateTime CreationTime { get;set; }
        public long? DeleterUserId { get;set; }
        public DateTime? DeletionTime { get;set; }
        public bool IsDeleted { get;set; }
        public long? LastModifierUserId { get;set; }
        public DateTime? LastModificationTime { get;set; }
    }
}
