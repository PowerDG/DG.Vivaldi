
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
using Abp.Extensions;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Application.Services.Dto;
using Abp.Linq.Extensions;


using DG.ERM.Products;
using DG.ERM.Products.Dtos;
using DG.ERM.Products.DomainService;
using DG.ERM.Products.Authorization;
using Dg.ERM;

using Abp.AutoMapper;
using Abp.Domain.Uow;

namespace DG.ERM.Products
{
    /// <summary>
    /// Product应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class ProductAppService : ERMAppServiceBase, IProductAppService
    {
        private readonly IRepository<Product, int> _entityRepository;

        private readonly IProductManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public ProductAppService(
        IRepository<Product, int> entityRepository
        , IProductManager entityManager
        )
        {
            _entityRepository = entityRepository;
            _entityManager = entityManager;
        }
        public IList<ProductEditDto> GetAllMissions()
        {
            var task = _entityRepository.GetAll().OrderByDescending(t => t.Id);
            return AutoMapper.Mapper.Map<List<ProductEditDto>>(task);
        }

        /// <summary>
        /// 获取Product的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		[AbpAuthorize(ProductPermissions.Query)]
        public async Task<PagedResultDto<ProductListDto>> GetPaged(GetProductsInput input)
        {

            var query = _entityRepository.GetAll();
            // TODO:根据传入的参数添加过滤条件


            var count = await query.CountAsync();

            var entityList = await query
                    .OrderBy(input.Sorting).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();

            // var entityListDtos = ObjectMapper.Map<List<ProductListDto>>(entityList);
            var entityListDtos = entityList.MapTo<List<ProductListDto>>();

            return new PagedResultDto<ProductListDto>(count, entityListDtos);
        }


        /// <summary>
        /// 通过指定id获取ProductListDto信息
        /// </summary>
        [AbpAuthorize(ProductPermissions.Query)]
        public async Task<ProductListDto> GetById(EntityDto<int> input)
        {
            var entity = await _entityRepository.GetAsync(input.Id);

            return entity.MapTo<ProductListDto>();
        }

        /// <summary>
        /// 获取编辑 Product
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(ProductPermissions.Create, ProductPermissions.Edit)]
        public async Task<GetProductForEditOutput> GetForEdit(NullableIdDto<int> input)
        {
            var output = new GetProductForEditOutput();
            ProductEditDto editDto;

            if (input.Id.HasValue)
            {
                var entity = await _entityRepository.GetAsync(input.Id.Value);

                editDto = entity.MapTo<ProductEditDto>();

                //productEditDto = ObjectMapper.Map<List<productEditDto>>(entity);
            }
            else
            {
                editDto = new ProductEditDto();
            }

            output.Product = editDto;
            return output;
        }


        /// <summary>
        /// 添加或者修改Product的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(ProductPermissions.Create, ProductPermissions.Edit)]
        public async Task CreateOrUpdate(CreateOrUpdateProductInput input)
        {

            if (input.Product.Id.HasValue)
            {
                await Update(input.Product);
            }
            else
            {
                await Create(input.Product);
            }
        }


        /// <summary>
        /// 新增Product
        /// </summary>
        [AbpAuthorize(ProductPermissions.Create)]
        protected virtual async Task<ProductEditDto> Create(ProductEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <Product>(input);
            //var entity=input.MapTo<Product>();


            var task = AutoMapper.Mapper.Map<Product>(input);
            var entity = await _entityRepository.InsertAsync(task);

            return AutoMapper.Mapper.Map<ProductEditDto>(entity);
            //return entity.MapTo<ProductEditDto>();
        }

        [UnitOfWork]
        public ProductEditDto CreateMission(ProductEditDto input)
        {
            var task = AutoMapper.Mapper.Map<Product>(input);

            var entity = _entityRepository.Insert(task);
            CurrentUnitOfWork.SaveChanges();
            return AutoMapper.Mapper.Map<ProductEditDto>(entity);

        }
        [UnitOfWork]
        public int? CreateMissionQ(ProductEditDto input)
        {
            var task = AutoMapper.Mapper.Map<Product>(input);
            //task.Id = null;
            if (task != null)
            {
                var result = _entityRepository.Insert(task);
                CurrentUnitOfWork.SaveChanges();

                return result.Id;
            }
            else
            { return 0; }
        }
        public ProductEditDto UpdateMission(ProductEditDto input)
        {
            // var task = _AfficheRepository.GetAll().FirstOrDefault(t => t.CargoID == input.CargoID);

            var task = AutoMapper.Mapper.Map<Product>(input);
            //_entityRepository.Update(task);
            //if (result != null)
            //{ _AfficheRepository.Update(result); }
            var entity = _entityRepository.Update(task);
            CurrentUnitOfWork.SaveChanges();
            return AutoMapper.Mapper.Map<ProductEditDto>(entity);
        }
        /// <summary>
        /// 编辑Product
        /// </summary>
        [AbpAuthorize(ProductPermissions.Edit)]
        protected virtual async Task Update(ProductEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = AutoMapper.Mapper.Map<Product>(input);
            //         var entity = await _entityRepository.GetAsync(input.Id.Value);
            //input.MapTo(entity);
            //var a= _entityRepository.UpdateAsync(entity)
            // ObjectMapper.Map(input, entity);
            await _entityRepository.UpdateAsync(entity);
        }



        /// <summary>
        /// 删除Product信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(ProductPermissions.Delete)]
        public async Task Delete(EntityDto<int> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(input.Id);
        }

        public ProductEditDto DeleteMission(int taskId)
        {
            //var task = _AfficheRepository.FirstOrDefault(t => t.Id == taskId);
            //if (task != null)
            //{ _AfficheRepository.Delete(task); }

            var entity = _entityRepository.Get(taskId);
            var task = AutoMapper.Mapper.Map<Product>(entity);
            //_entityRepository.Update(task);
            //if (result != null)
            //{ _AfficheRepository.Update(result); }
            //var entity = _entityRepository.Update(task);
            //CurrentUnitOfWork.SaveChanges();
            _entityRepository.Delete(task.Id);
            return AutoMapper.Mapper.Map<ProductEditDto>(entity);
        }

        /// <summary>
        /// 批量删除Product的方法
        /// </summary>
        [AbpAuthorize(ProductPermissions.BatchDelete)]
        public async Task BatchDelete(List<int> input)
        {
            // TODO:批量删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
        }


        /// <summary>
        /// 导出Product为excel表,等待开发。
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


