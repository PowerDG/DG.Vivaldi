
using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using DG.ERM.Products;

namespace  DG.ERM.Products.Dtos
{


    [AutoMap(typeof(Product))]
    public class ProductEditDto
    {

        /// <summary>
        /// Id
        /// </summary>
        public new int? Id { get; set; }         


        
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




    }
}