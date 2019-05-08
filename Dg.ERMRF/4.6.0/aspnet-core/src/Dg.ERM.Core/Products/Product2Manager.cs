using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.Organizations;
using Dg.ERM.Authorization.Users; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DG.ERM.Products
{
    public class Product2Manager : IDomainService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;


        private readonly UserManager _userManager;

        public Product2Manager(
        IRepository<Product> productRepository,    
        IRepository<OrganizationUnit, long> organizationUnitRepository,

        UserManager userManager)
    {
        _productRepository = productRepository;
        _organizationUnitRepository = organizationUnitRepository;
        _userManager = userManager;
    }

//        2. 获取某个组织单位内的实体

//获取某个组织单位内的实体很简单，请看如下领域服务中的示例代码：
        public List<Product> GetProductsInOu(long organizationUnitId)
        {
            return _productRepository.GetAllList(p => p.OrganizationUnitId == organizationUnitId);
        }
//        3. 获取某个组织单位及其子组织单位内的实体

//当我们想获取某个组织单位及其子组织单位内的实体的时候，OU编码就可以帮我们实现这个功能了。
        [UnitOfWork]
        public virtual List<Product> GetProductsInOuIncludingChildren(long organizationUnitId)
        {
            var code = _organizationUnitRepository.Get(organizationUnitId).Code;

            var query =
                from product in _productRepository.GetAll()
                join organizationUnit in _organizationUnitRepository.GetAll() on product.OrganizationUnitId equals organizationUnit.Id
                where organizationUnit.Code.StartsWith(code)
                select product;

            return query.ToList();
        }

//        4. 过滤某个用户的实体

//我们可能希望获取某个用户所属的OU（一个或多个）内的所有产品，代码如下：
        public async Task<List<Product>> GetProductsForUserAsync(long userId)
        {
            var user = await _userManager.GetUserByIdAsync(userId);
            var organizationUnits = await _userManager.GetOrganizationUnitsAsync(user);
            var organizationUnitIds = organizationUnits.Select(ou => ou.Id);

            return await _productRepository.GetAllListAsync(p => organizationUnitIds.Contains(p.OrganizationUnitId));
        }
        //获取某个用户所属的OU（一个或多个）内及其子OU内的所有产品
        [UnitOfWork]
        public virtual async Task<List<Product>> GetProductsForUserIncludingChildOusAsync(long userId)
        {
            var user = await _userManager.GetUserByIdAsync(userId);
            var organizationUnits = await _userManager.GetOrganizationUnitsAsync(user);
            var organizationUnitCodes = organizationUnits.Select(ou => ou.Code);

            var query =
                from product in _productRepository.GetAll()
                join organizationUnit in _organizationUnitRepository.GetAll() on product.OrganizationUnitId equals organizationUnit.Id
                where organizationUnitCodes.Any(code => organizationUnit.Code.StartsWith(code))
                select product;

            return query.ToList();
        }












    }
}
