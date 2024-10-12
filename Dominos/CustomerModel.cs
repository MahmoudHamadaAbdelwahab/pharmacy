using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Dominos.Models
{
    public class CustomerModel
    {
        [Key]
        [ValidateNever]
        public int CustomerId { get; set; }
        [Required(ErrorMessage ="please enter name")]
        public string CustomerName { get; set; }
        [EmailAddress(ErrorMessage = "please enter valid email")]
        [StringLength(100, ErrorMessage = "email must be less than 100")]
        public string CustomerEmail { get; set; } = null;
        [Required(ErrorMessage = "Please type a password that is somewhat difficult\r\n")]
        public string Password { get; set; }
        [Required(ErrorMessage = "please enter name")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "please enter address name")]
        public string Address { get; set; }

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

        public ICollection<OrderModel> TbOrder { get; set; }
    }
}
