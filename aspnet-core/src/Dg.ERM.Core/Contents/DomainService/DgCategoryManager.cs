

using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Abp.Linq;
using Abp.Linq.Extensions;
using Abp.Extensions;
using Abp.UI;
using Abp.Domain.Repositories;
using Abp.Domain.Services;

using Dg.ERM;
using Dg.ERM.Contents;


namespace Dg.ERM.Contents.DomainService
{
    /// <summary>
    /// DgCategory领域层的业务管理
    ///</summary>
    public class DgCategoryManager :ERMDomainServiceBase, IDgCategoryManager
    {
		
		private readonly IRepository<DgCategory,long> _repository;

		/// <summary>
		/// DgCategory的构造方法
		///</summary>
		public DgCategoryManager(
			IRepository<DgCategory, long> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitDgCategory()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
