using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace DgCore.DgEnitity
{
    public class LogisticsInfo :Entity<int>,ICreationAudited
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new int? Id { get; set; }

        public long BillNo { get; set; }
        public int State { get; set; }
        public string FillDate { get; set; }
        public string Infomation { get; set; }

        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
