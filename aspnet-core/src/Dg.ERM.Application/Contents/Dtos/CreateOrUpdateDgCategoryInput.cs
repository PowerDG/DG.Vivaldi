

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Dg.ERM.Contents;

namespace Dg.ERM.Contents.Dtos
{
    public class CreateOrUpdateDgCategoryInput
    {
        [Required]
        public DgCategoryEditDto DgCategory { get; set; }

    }
}