
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


using Dg.ERM.Authorization.ExtendInfos;
using Dg.ERM.Authorization.ExtendInfos.Dtos;




namespace Dg.ERM.Authorization.ExtendInfos
{
    /// <summary>
    /// ExtendInfo应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class ExtendInfoAppService : ERMAppServiceBase, IExtendInfoAppService
    {
        private readonly IRepository<ExtendInfo, long> _entityRepository;

        

        /// <summary>
        /// 构造函数 
        ///</summary>
        public ExtendInfoAppService(
        IRepository<ExtendInfo, long> entityRepository
        
        )
        {
            _entityRepository = entityRepository; 
            
        }


        /// <summary>
        /// 获取ExtendInfo的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<ExtendInfoListDto>> GetPaged(GetExtendInfosInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<ExtendInfoListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<ExtendInfoListDto>>();

			return new PagedResultDto<ExtendInfoListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取ExtendInfoListDto信息
		/// </summary>
		 
		public async Task<ExtendInfoListDto> GetById(EntityDto<long> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<ExtendInfoListDto>();
		}

		/// <summary>
		/// 获取编辑 ExtendInfo
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetExtendInfoForEditOutput> GetForEdit(NullableIdDto<long> input)
		{
			var output = new GetExtendInfoForEditOutput();
ExtendInfoEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<ExtendInfoEditDto>();

				//extendInfoEditDto = ObjectMapper.Map<List<extendInfoEditDto>>(entity);
			}
			else
			{
				editDto = new ExtendInfoEditDto();
			}

			output.ExtendInfo = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改ExtendInfo的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task CreateOrUpdate(CreateOrUpdateExtendInfoInput input)
		{

			if (input.ExtendInfo.Id.HasValue)

            {
				await Update(input.ExtendInfo);
			}
			else
			{
				await Create(input.ExtendInfo);
			}
		}


        /// <summary>
        /// 新增ExtendInfo
        /// </summary>



        public  ExtendInfoEditDto InsertToInfo(ExtendInfoEditDto input)
        {

            var entity = AutoMapper.Mapper.Map<ExtendInfo>(input); 
            entity.Id = null;
            if (entity!=null)
            {
                _entityRepository.Insert(entity);

                CurrentUnitOfWork.SaveChanges();
                return AutoMapper.Mapper.Map<ExtendInfoEditDto>(input);
            }
            else
            {
                return null;
            }
        }
        public ExtendInfoEditDto UpdateInfo(ExtendInfoEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新 
                 _entityRepository.Update(AutoMapper.Mapper.Map<ExtendInfo>(input));
            return input;
        }
        public async Task DeleteInfo(EntityDto<long> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(input.Id);
        }
        public IList<ExtendInfoEditDto> GetAllMissions()
        {
            var task = _entityRepository.GetAll().OrderByDescending(t => t.Id);
            return AutoMapper.Mapper.Map<List<ExtendInfoEditDto>>(task);
        }


        protected virtual async Task<ExtendInfoEditDto> Create(ExtendInfoEditDto input)
		{
			////TODO:新增前的逻辑判断，是否允许新增

   //         // var entity = ObjectMapper.Map <ExtendInfo>(input);
   //         var entity=input.MapTo<ExtendInfo>();
			

			//entity = await _entityRepository.InsertAsync(entity);
			//return entity.MapTo<ExtendInfoEditDto>();


            var task = AutoMapper.Mapper.Map<ExtendInfo>(input);

            var entity = _entityRepository.Insert(task);

            CurrentUnitOfWork.SaveChanges();
            return AutoMapper.Mapper.Map<ExtendInfoEditDto>(input);
        }

        /// <summary>
        /// 编辑ExtendInfo
        /// </summary>
   
        protected virtual async Task Update(ExtendInfoEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			//var entity = await _entityRepository.GetAsync(input.Id.Value);
			//input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(AutoMapper.Mapper.Map<ExtendInfo>(input));
		}



		/// <summary>
		/// 删除ExtendInfo信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<long> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除ExtendInfo的方法
		/// </summary>
		
		public async Task BatchDelete(List<long> input)
		{
            // TODO:批量删除前的逻辑判断，是否允许删除

            await _entityRepository.CountAsync();
		}

      

        /// <summary>
        /// 导出ExtendInfo为excel表,等待开发。
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


