

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
using DG.ERM.Products;


namespace DG.ERM.Products.DomainService
{
    /// <summary>
    /// Product领域层的业务管理
    ///</summary>
    public class ProductManager :ERMDomainServiceBase, IProductManager
    {
		
		private readonly IRepository<Product,int> _repository;

		/// <summary>
		/// Product的构造方法
		///</summary>
		public ProductManager(
			IRepository<Product, int> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitProduct()
		{
			throw new NotImplementedException();
		}

		// TODO:编写领域业务代码



		 
		  
		 

	}
}
