
using Abp.Runtime.Validation;
using Dg.ERM.Dtos;
using Dg.ERM.Contents;

namespace Dg.ERM.Contents.Dtos
{
    public class GetDgCategorysInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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
