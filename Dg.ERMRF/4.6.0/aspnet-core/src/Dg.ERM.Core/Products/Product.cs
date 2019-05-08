using Abp.Domain.Entities;
using Abp.Organizations;
using System;
using System.Collections.Generic;
using System.Text;

namespace DG.ERM.Products
{
    public class Product : Entity, IMustHaveTenant, IMustHaveOrganizationUnit, ISoftDelete
    {
        public virtual int TenantId { get; set; }

        public virtual long OrganizationUnitId { get; set; }

        public virtual string Name { get; set; }

        public virtual float Price { get; set; }
        public bool IsDeleted { get; set; }
    }
     
}
