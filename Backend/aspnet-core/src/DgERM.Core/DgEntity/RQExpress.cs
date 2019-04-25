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
   public class Express:Entity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new int? Id { get; set; }
        public string Type { get; set; }
        public string Province { get; set; }
        public int Price_Kg { get; set; }
        public int Price_Kg_One { get; set; }
        public string Aging { get; set; }
    }
}
