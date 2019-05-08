
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


using Dg.ERM.Contents.Dtos;
using Dg.ERM.Contents;

namespace Dg.ERM.Contents
{
    /// <summary>
    /// DgCategory应用层服务的接口方法
    ///</summary>
    public interface IDgCategoryAppService : IApplicationService
    {
        /// <summary>
		/// 获取DgCategory的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<DgCategoryListDto>> GetPaged(GetDgCategorysInput input);


		/// <summary>
		/// 通过指定id获取DgCategoryListDto信息
		/// </summary>
		Task<DgCategoryListDto> GetById(EntityDto<long> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetDgCategoryForEditOutput> GetForEdit(NullableIdDto<long> input);


        /// <summary>
        /// 添加或者修改DgCategory的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateDgCategoryInput input);


        /// <summary>
        /// 删除DgCategory信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<long> input);


        /// <summary>
        /// 批量删除DgCategory
        /// </summary>
        Task BatchDelete(List<long> input);


		/// <summary>
        /// 导出DgCategory为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetToExcel();

    }
}
