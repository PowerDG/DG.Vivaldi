
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
using Abp.Extensions;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Application.Services.Dto;
using Abp.Linq.Extensions;

 
using Dg.ERM.Contents;
using Dg.ERM.Contents.Dtos;
using Dg.ERM.Contents.DomainService;



namespace Dg.ERM.Contents
{
    /// <summary>
    /// DgCategory应用层服务的接口实现方法  
    ///</summary>///
    /// 

     

    [AbpAuthorize]
    public class DgCategoryAppService : ERMAppServiceBase, IDgCategoryAppService
    {
        private readonly IRepository<DgCategory, long> _entityRepository;

        private readonly IDgCategoryManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public DgCategoryAppService(
        IRepository<DgCategory, long> entityRepository
        ,IDgCategoryManager entityManager
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
        }
        /// <summary>
        /// /InsertOrUpdateAndGetId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public long CreateMissionQ(DgCategoryEditDto input)
        {
            var task = AutoMapper.Mapper.Map<DgCategory>(input);
            if (task != null)
            {
                long  result = _entityRepository.InsertOrUpdateAndGetId(task);
                return result;
            }
            else
            { return 0;
            }
        }


        /// <summary>
        /// 获取DgCategory的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<PagedResultDto<DgCategoryListDto>> GetPaged(GetDgCategorysInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<DgCategoryListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<DgCategoryListDto>>();

			return new PagedResultDto<DgCategoryListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取DgCategoryListDto信息
		/// </summary>
		 
		public async Task<DgCategoryListDto> GetById(EntityDto<long> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<DgCategoryListDto>();
		}

		/// <summary>
		/// 获取编辑 DgCategory
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetDgCategoryForEditOutput> GetForEdit(NullableIdDto<long> input)
		{
			var output = new GetDgCategoryForEditOutput();
DgCategoryEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<DgCategoryEditDto>();

				//dgCategoryEditDto = ObjectMapper.Map<List<dgCategoryEditDto>>(entity);
			}
			else
			{
				editDto = new DgCategoryEditDto();
			}

			output.DgCategory = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改DgCategory的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task CreateOrUpdate(CreateOrUpdateDgCategoryInput input)
		{

			if (input.DgCategory.Id.HasValue)
			{
				await Update(input.DgCategory);
			}
			else
			{
				await Create(input.DgCategory);
			}
		}


		/// <summary>
		/// 新增DgCategory
		/// </summary>
		
		protected virtual async Task<DgCategoryEditDto> Create(DgCategoryEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <DgCategory>(input);
            var entity=input.MapTo<DgCategory>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<DgCategoryEditDto>();
		}

		/// <summary>
		/// 编辑DgCategory
		/// </summary>
		
		protected virtual async Task Update(DgCategoryEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除DgCategory信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<long> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除DgCategory的方法
		/// </summary>
		
		public async Task BatchDelete(List<long> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


		/// <summary>
		/// 导出DgCategory为excel表,等待开发。
		/// </summary>
		/// <returns></returns>
		//public async Task<FileDto> GetToExcel()
		//{
		//	var users = await UserManager.Users.ToListAsync();
		//	var userListDtos = ObjectMapper.Map<List<UserListDto>>(users);
		//	await FillRoleNames(userListDtos);
		//	return _userListExcelExporter.ExportToFile(userListDtos);
		//}

    }
}


