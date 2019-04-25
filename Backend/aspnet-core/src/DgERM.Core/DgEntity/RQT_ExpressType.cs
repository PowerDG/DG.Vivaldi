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
    public class T_ExpressType:Entity<int>
    {
      
        public new int?Id { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ExpressNo { get; set; }
        public string ExpressName { get; set; }
        public string ExpressICO { get; set; }
        public string Remark { get; set; }
        public bool IsDefaultShow { get; set; }

        public int Sorting { get; set; }
    }
}
