
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using Dg.ERM.Contents;

using Abp.AutoMapper;
namespace  Dg.ERM.Contents.Dtos
{
    [AutoMap(typeof(DgCategory))]
    public class DgCategoryEditDto
    {
        public const int MaxStringLength = 255;
        public const int MaxContentLength = 4000;
        /// <summary>
        /// Id
        /// </summary>
        public long? Id { get; set; }         


        
		/// <summary>
		/// ParentId
		/// </summary>
		public long? ParentId { get; set; }



		/// <summary>
		/// Title
		/// </summary>
[MaxLength(MaxStringLength)]
		[Required(ErrorMessage="Title不能为空")]
		public string Title { get; set; }



		/// <summary>
		/// Image
		/// </summary>
[MaxLength(MaxStringLength)]
		public string Image { get; set; }



		/// <summary>
		/// Content
		/// </summary>
[MaxLength(MaxContentLength)]
		public string Content { get; set; }



        public int TenantId { get; set; }
        /// <summary>
        /// SortOrder
        /// </summary>
        public int SortOrder { get; set; }




    }
}