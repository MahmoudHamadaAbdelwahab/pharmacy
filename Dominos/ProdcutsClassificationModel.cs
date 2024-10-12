using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Dominos.Models
{
    public class ProdcutsClassificationModel
    {
        [Key]
        [ValidateNever]
        public int ProdcutsClassificationId { get; set; }
        [Required]
        public string ProdcutsClassificationName { get; set; }

        // start columne secuirty
        [ValidateNever]
        public string CreatedBy { get; set; }
        [ValidateNever]
        public DateTime CreatedDate { get; set; }
        [ValidateNever]
        public int CurrentState { get; set; }
        [ValidateNever]
        public string? UpdatedBy { get; set; }
        [ValidateNever]
        public DateTime? UpdatedDate { get; set; }
        // end columne secuirty

        public ICollection<ProductsModel> TbProducts { get; set; }
    }
}
