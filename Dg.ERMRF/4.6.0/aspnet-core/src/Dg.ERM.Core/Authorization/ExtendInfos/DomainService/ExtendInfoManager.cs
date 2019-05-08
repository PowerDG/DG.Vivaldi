

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
using Dg.ERM.Authorization.ExtendInfos;


namespace Dg.ERM.Authorization.ExtendInfos.DomainService
{
    /// <summary>
    /// ExtendInfo领域层的业务管理
    ///</summary>
    public class ExtendInfoManager :ERMDomainServiceBase, IExtendInfoManager
    {
		
		private readonly IRepository<ExtendInfo,long> _repository;

		/// <summary>
		/// ExtendInfo的构造方法
		///</summary>
		public ExtendInfoManager(
			IRepository<ExtendInfo, long> repository
		)
		{
			_repository =  repository;
		}


		/// <summary>
		/// 初始化
		///</summary>
		public void InitExtendInfo()
		{
			throw new NotImplementedException();
		}

        // TODO:编写领域业务代码


        public ExtendInfo InsertToInfo(ExtendInfo input)
        {

            //var entity = AutoMapper.Mapper.Map<ExtendInfo>(input);
            input.Id = null;
            if (input != null)
            {
                _repository.Insert(input);

                CurrentUnitOfWork.SaveChanges();
                return input;// AutoMapper.Mapper.Map<ExtendInfo>(input);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="EntityTypeFullName"></param>
        /// <param name="EnityID"></param>
        /// <returns></returns>
        public ExtendInfo BindToInfo(ExtendInfo input,string EntityTypeFullName, string EnityID)
        {

            //var entity = AutoMapper.Mapper.Map<ExtendInfo>(input);
            input.Id = null;
            if (input != null)
            {
                _repository.Insert(input);

                CurrentUnitOfWork.SaveChanges();
                return input;// AutoMapper.Mapper.Map<ExtendInfo>(input);
            }
            else
            {
                return null;
            }
        }

        public ExtendInfo UpdateInfo(ExtendInfo input)
        {
            //TODO:更新前的逻辑判断，是否允许更新 
            _repository.Update(AutoMapper.Mapper.Map<ExtendInfo>(input));

            _repository.Update(input);
            return input;
        }
        public async Task DeleteInfo( long input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _repository.DeleteAsync(input);
        }
        //public IList<ExtendInfo> GetAllMissions()
        //{
        //    var task = _repository.GetAll().OrderByDescending(t => t.Id);
        //    return AutoMapper.Mapper.Map<List<ExtendInfo>>(task);
        //}




    }
}
