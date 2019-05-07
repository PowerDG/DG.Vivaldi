
using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using Dg.ERM.Authorization.ExtendInfos;

namespace  Dg.ERM.Authorization.ExtendInfos.Dtos
{

    [AutoMap(typeof(ExtendInfo))]
    public class ExtendInfoDto
    {

        /// <summary>
        /// Id
        /// </summary>
        //public long? Id { get; set; }



        /// <summary>
        /// TenantId
        /// </summary>
        public int TenantId { get; set; }
         
        ///// <summary>
        ///// ParentId
        ///// </summary>
        //public int ParentId { get; set; } 
        /// <summary>
        /// Super_Type
        /// </summary>
        public string Super_Type { get; set; }
        ///// <summary>
        ///// EntityTypeFullName
        ///// </summary>
        //public string EntityTypeFullName { get; set; }


        public long EnityID { get; set; }

        ///// <summary>
        ///// Name
        ///// </summary>
        ////[Required(ErrorMessage = "Name不能为空")]
        //public string Name { get; set; }


        ///// <summary>
        ///// Value
        ///// </summary>
        //public string Value { get; set; }

        /// <summary>
        /// DataTypeName
        /// </summary>
        //public string DataTypeName { get; set; }



        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }

        public string PropertyTypeName { get; set; }

        ///// <summary>
        ///// Description
        ///// </summary>
        //public string Description { get; set; }
        ///// <summary>
        ///// DisplayName
        ///// </summary>
        //public string DisplayName { get; set; }




    }











    [AutoMap(typeof(ExtendInfo))]
    public class ExtendInfoEditDto
    {

        /// <summary>
        /// Id
        /// </summary>
        public    long? Id { get; set; }         


        
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



        public long EnityID { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }

        public string PropertyTypeName { get; set; }


        /// <summary>
        /// DisplayName
        /// </summary>
        public string DisplayName { get; set; }




    }
}