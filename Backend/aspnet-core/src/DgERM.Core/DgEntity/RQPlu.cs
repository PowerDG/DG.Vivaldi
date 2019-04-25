using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using DgCore.DgEnitity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DgCore.DgEnitity
{
   public class Plu:Entity<int>
   {     [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new int? Id { get; set; }

        public string Province { get; set; }
        public string Destination_city { get; set; }
        public string Aging { get; set; }
        
        public decimal Price_Kg { get; set; }
        public decimal Price_Cu { get; set; }
    }
}
