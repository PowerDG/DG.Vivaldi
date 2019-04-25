using System;
using System.Collections.Generic;
using System.Text;


using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;  
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 
namespace DgCore.DgEnitity
{
    public class Balance : Entity<int>, ICreationAudited
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new int? Id { get; set; }

        public uint BalanceNo { get; set; }

        public string Claime_Type { get; set; }
        public string Super_Type { get; set; }
        public string Claim_Name { get; set; }



        public decimal TotalFee { get; set; }
        public int CustomerID { get; set; }
        [MaxLength(255)]
        public string CompanyName { get; set; }
        public int BranchID { get; set; }
        public string TypeName { get; set; }
        public int Status { get; set; }
        public int Sorting { get; set; }
        public long? CreatorUserId {get;set;}
        public DateTime CreationTime {get;set;}
    }
}
