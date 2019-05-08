
using Abp.Runtime.Validation;
using Dg.ERM.Dtos;
using DG.ERM.Products;

namespace DG.ERM.Products.Dtos
{
    public class GetProductsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {

        /// <summary>
        /// 正常化排序使用
        /// </summary>
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "Id";
            }
        }

    }
}
