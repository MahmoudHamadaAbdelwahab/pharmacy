using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Dominos.Models
{
    public class SupplierModel
    {
        [Key]
        [ValidateNever]
        public int SupplierId { get; set; }
        [Required(ErrorMessage ="please enter name")]
        public string SupplierName { get; set; }
        [Required(ErrorMessage = "please enter contact info")]
        public string Contact_Info { get; set; }

        // start colume security
        [ValidateNever]
        public string CreatedBy { get; set; } = null!;
        [ValidateNever]
        public DateTime CreatedDate { get; set; }
        [ValidateNever]
        public int CurrentState { get; set; }
        [ValidateNever]
        public string? UpdatedBy { get; set; }
        [ValidateNever]
        public DateTime? UpdatedDate { get; set; }
        // end colume security

        [ValidateNever] // Exclude TbProducts from model validation
        public ProductsModel TbProducts { get; set; }
    }
}
