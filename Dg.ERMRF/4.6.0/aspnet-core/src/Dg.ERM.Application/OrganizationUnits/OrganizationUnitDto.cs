using Abp.AutoMapper;
using Abp.Organizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dg.ERM.OrganizationUnits
{
    [AutoMap(typeof(OrganizationUnit))]
    public class OrganizationUnitDto
    {        //
        // 摘要:
        //     Hierarchical Code of this organization unit. Example: "00001.00042.00005". This
        //     is a unique code for a Tenant. It's changeable if OU hierarch is changed.
        
        public virtual string Code { get; set; }
        //
        // 摘要:
        //     Parent Abp.Organizations.OrganizationUnit Id. Null, if this OU is root.
        public virtual long? ParentId { get; set; }
        //
        // 摘要:
        //     Parent Abp.Organizations.OrganizationUnit. Null, if this OU is root.
 
        //public virtual OrganizationUnit Parent { get; set; }
        //
        // 摘要:
        //     TenantId of this entity.
        public virtual int? TenantId { get; set; }
        //
        // 摘要:
        //     Children of this OU.
        //public virtual ICollection<OrganizationUnit> Children { get; set; } 
        public virtual string DisplayName { get; set; }
    }
}
