
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DgCore.DgEnitity
{
    public class TruckModel : Entity<int>, ICreationAudited, IDeletionAudited
    {
        [Key]
        [Required]
        public int TMID { get; set; }

       
        [Required]
        [MaxLength(255)]
        public string TMName { get; set; }
      
        [Required]
        public string TMWeight { get; set; }
        
        [Required]
        public string UTMCubage { get; set; }
        public int TMPassenger { get; set; }


        #region 数据版本号
        public int VersionCode { get; set; }
        #endregion
        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }

        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }

    }
}
