

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using Dg.ERM.Authorization.ExtendInfos;
using Abp.AutoMapper;

namespace Dg.ERM.Authorization.ExtendInfos.Dtos
{
    [AutoMap(typeof(ExtendInfo))]
    public class ExtendInfoListDto  
    {

        
        public new long? Id { get; set; }
		/// <summary>
		/// TenantId
		/// </summary>
		public int TenantId { get; set; }



		/// <summary>
		/// ParentId
		/// </summary>
		public int ParentId { get; set; }



		/// <summary>
		/// Super_Type
		/// </summary>
		public string Super_Type { get; set; }



		/// <summary>
		/// EntityTypeFullName
		/// </summary>
		public string EntityTypeFullName { get; set; }



		/// <summary>
		/// Name
		/// </summary>
		[Required(ErrorMessage="Name不能为空")]
		public string Name { get; set; }



		/// <summary>
		/// Value
		/// </summary>
		public string Value { get; set; }



		/// <summary>
		/// DataTypeName
		/// </summary>
		public string DataTypeName { get; set; }



		/// <summary>
		/// Description
		/// </summary>
		public string Description { get; set; }



		/// <summary>
		/// DisplayName
		/// </summary>
		public string DisplayName { get; set; }



		/// <summary>
		/// CreatorUserId
		/// </summary>
		public long? CreatorUserId { get; set; }



		/// <summary>
		/// CreationTime
		/// </summary>
		public DateTime CreationTime { get; set; }



		/// <summary>
		/// IsDeleted
		/// </summary>
		public bool IsDeleted { get; set; }




    }
}