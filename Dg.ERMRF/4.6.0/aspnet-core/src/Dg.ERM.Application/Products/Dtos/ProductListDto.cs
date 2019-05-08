

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using DG.ERM.Products;
using Abp.Domain.Entities;
using Abp.Organizations;

namespace DG.ERM.Products.Dtos
{
    public class ProductListDto : EntityDto,IMustHaveTenant,IMustHaveOrganizationUnit,ISoftDelete 
    {

        
		/// <summary>
		/// TenantId
		/// </summary>
		public int TenantId { get; set; }



		/// <summary>
		/// OrganizationUnitId
		/// </summary>
		public long OrganizationUnitId { get; set; }



		/// <summary>
		/// Name
		/// </summary>
		public string Name { get; set; }



		/// <summary>
		/// Price
		/// </summary>
		public float Price { get; set; }



		/// <summary>
		/// IsDeleted
		/// </summary>
		public bool IsDeleted { get; set; }




    }
}