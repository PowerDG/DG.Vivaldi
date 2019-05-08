
using Abp.Runtime.Validation;
using Dg.ERM.Dtos;
using Dg.ERM.Authorization.ExtendInfos;

namespace Dg.ERM.Authorization.ExtendInfos.Dtos
{
    public class GetExtendInfosInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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
