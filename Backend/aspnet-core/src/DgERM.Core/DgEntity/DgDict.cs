using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DgCore.DgEnitity
{
    public class DgDict
    {


    }

    public class DgDictionary : Entity<int>, ICreationAudited, IDeletionAudited
    {
        //    public string Status ;

        //public int StatueNo { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new int? Id { get; set; }
        public string Subject { get; set; }

        public string Claim_Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string Claim_Value { get; set; }

        public string Issuer { get; set; }
        public string Claim_Type { get; set; }
        public string Super_Type { get; set; }
        public string Sub_Type { get; set; }

        public int Sorting { get; set; }
        public bool isDisplay { get; set; }

        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
    };

    public struct TrafficLog  
    {
        //    public string Status ;

        public int StatueNo;
        public string displayName;
        public string description;
        public TrafficLog(int No, string Name, string Des)
        {
            StatueNo = No; displayName = Name; description = Des;
        }
    };

    public class DgDictSelect
    {
        public string Claim_Name { get; set; }
        public string DisplayName { get; set; }
    }
}
