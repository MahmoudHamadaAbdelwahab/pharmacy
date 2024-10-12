using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Dominos.Models
{
    public class ImagesModel
    {
        [Key]
        [ValidateNever]
        public int ImageId { get; set; }
        [Required(ErrorMessage = "Please select a Images")]
        public string ImageName { get; set; } = null!;
        [Required(ErrorMessage = "Please select a Product")]
        public int ProductId { get; set; }
        [ValidateNever]
        public ProductsModel TbProducts { get; set; }
    }
}
