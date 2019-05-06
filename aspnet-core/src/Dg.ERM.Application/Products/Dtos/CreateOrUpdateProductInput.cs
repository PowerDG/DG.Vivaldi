

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DG.ERM.Products;

namespace DG.ERM.Products.Dtos
{
    public class CreateOrUpdateProductInput
    {
        [Required]
        public ProductEditDto Product { get; set; }

    }
}