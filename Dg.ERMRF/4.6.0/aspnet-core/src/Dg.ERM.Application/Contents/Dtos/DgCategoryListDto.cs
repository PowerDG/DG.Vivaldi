

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using Dg.ERM.Contents;
using Abp.Domain.Entities;

namespace Dg.ERM.Contents.Dtos
{
    public class DgCategoryListDto : EntityDto<long>,IFullAudited,IMustHaveTenant 
    {
        public const int MaxStringLength = 255;
        public const int MaxContentLength = 4000;

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



		/// <summary>
		/// SortOrder
		/// </summary>
		public int SortOrder { get; set; }



		/// <summary>
		/// CreationTime
		/// </summary>
		public DateTime CreationTime { get; set; }



		/// <summary>
		/// LastModificationTime
		/// </summary>
		public DateTime? LastModificationTime { get; set; }



		/// <summary>
		/// DeletionTime
		/// </summary>
		public DateTime? DeletionTime { get; set; }



		/// <summary>
		/// CreatorUserId
		/// </summary>
		public long? CreatorUserId { get; set; }



		/// <summary>
		/// LastModifierUserId
		/// </summary>
		public long? LastModifierUserId { get; set; }



		/// <summary>
		/// DeleterUserId
		/// </summary>
		public long? DeleterUserId { get; set; }



		/// <summary>
		/// IsDeleted
		/// </summary>
		public bool IsDeleted { get; set; }



		/// <summary>
		/// TenantId
		/// </summary>
		public int TenantId { get; set; }




    }
}