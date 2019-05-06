
using System;
using System.Data;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using Abp.UI;
using Abp.AutoMapper;
using Abp.Authorization;
using Abp.Linq.Extensions;
using Abp.Domain.Repositories;
using Abp.Application.Services;
using Abp.Application.Services.Dto;


using Dg.ERM.Authorization.ExtendInfos.Dtos;
using Dg.ERM.Authorization.ExtendInfos;

namespace Dg.ERM.Authorization.ExtendInfos
{
    /// <summary>
    /// ExtendInfo应用层服务的接口方法
    ///</summary>
    public interface IExtendInfoAppService : IApplicationService
    {
        IList<ExtendInfoEditDto> GetAllMissions();

        //ExtendInfoEditDto UpdateInfo(ExtendInfoEditDto input);
        //ExtendInfoEditDto InsertToInfo(ExtendInfoEditDto input);
        /// <summary>
        /// 获取ExtendInfo的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<ExtendInfoListDto>> GetPaged(GetExtendInfosInput input);


		/// <summary>
		/// 通过指定id获取ExtendInfoListDto信息
		/// </summary>
		Task<ExtendInfoListDto> GetById(EntityDto<long> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetExtendInfoForEditOutput> GetForEdit(NullableIdDto<long> input);


        /// <summary>
        /// 添加或者修改ExtendInfo的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateExtendInfoInput input);


        /// <summary>
        /// 删除ExtendInfo信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<long> input);


        /// <summary>
        /// 批量删除ExtendInfo
        /// </summary>
        Task BatchDelete(List<long> input);


		/// <summary>
        /// 导出ExtendInfo为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetToExcel();

    }
}
