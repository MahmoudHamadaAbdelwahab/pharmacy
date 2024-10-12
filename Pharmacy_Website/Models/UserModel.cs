using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
namespace Pharmacy_Website.Models
{
    public class UserModel
    {
        [Required(ErrorMessage ="please enter first name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "please enter last name")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "please enter valid email")]
        [StringLength(100, ErrorMessage = "email must be less than 150")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please type a password that is somewhat difficult\r\n")]
        public string Password { get; set; }
        [ValidateNever]
        public string ReturnUrl { get; set; }
    }
}
