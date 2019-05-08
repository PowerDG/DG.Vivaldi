

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using Dg.ERM.Authorization.ExtendInfos;


namespace Dg.ERM.Authorization.ExtendInfos.DomainService
{
    public interface IExtendInfoManager : IDomainService
    {

        /// <summary>
        /// 初始化方法
        ///</summary>
        void InitExtendInfo();



		 
      
         

    }
}
