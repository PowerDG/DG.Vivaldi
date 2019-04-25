using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace DgCore.DgEnitity
{
    public class DepartmentInfo : Entity, ICreationAudited, IDeletionAudited
    {
        public new int? Id { get; set; }
        [Key]
        public int DepartmentID { get; set; }

        [MaxLength(255)]
        public string DepartmentName { get; set; }

        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
