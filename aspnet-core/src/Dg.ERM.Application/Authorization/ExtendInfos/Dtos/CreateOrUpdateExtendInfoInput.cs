

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Dg.ERM.Authorization.ExtendInfos;

namespace Dg.ERM.Authorization.ExtendInfos.Dtos
{
    public class CreateOrUpdateExtendInfoInput
    {
        [Required]
        public ExtendInfoEditDto ExtendInfo { get; set; }

    }
}