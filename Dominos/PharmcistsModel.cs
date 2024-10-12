using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Dominos.Models
{
    public class PharmcistsModel
    {
        [Key]
        [ValidateNever]
        public int PharmcistId { get; set; }

        [Required(ErrorMessage = "Please enter name")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string PharmcistName { get; set; }

        [Required(ErrorMessage = "Please enter email")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string PharmcistEmail { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 50 characters long")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter address")]
        [StringLength(200, ErrorMessage = "Address cannot be longer than 200 characters")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter phone number")]
        [Phone(ErrorMessage = "Please enter a valid phone number")]
        public string PhoneNumber { get; set; }

        [ValidateNever]
        public string Role { get; set; }

        // start columne secuirty
        [ValidateNever]
        public string CreatedBy { get; set; }
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
