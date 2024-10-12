using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Dominos.Models
{
    public class SettingsModel
    {
        [Key]
        [ValidateNever]
        public int SettingsId { get; set; }

        [Required(ErrorMessage = "Website name is required.")]
        [StringLength(100, ErrorMessage = "Website name cannot be longer than 100 characters.")]
        public string WebsiteName { get; set; }

        [StringLength(500, ErrorMessage = "About Us cannot be longer than 500 characters.")]
        public string AboutUs { get; set; }

        [StringLength(300, ErrorMessage = "Copy Right cannot be longer than 300 characters.")]
        public string CopyRight { get; set; }

        [Url(ErrorMessage = "Please enter a valid Facebook link.")]
        public string FacebookLink { get; set; }

        [Url(ErrorMessage = "Please enter a valid Twitter link.")]
        public string TwitterLink { get; set; }

        [Url(ErrorMessage = "Please enter a valid Instagram link.")]
        public string InstgramLink { get; set; }

        [Url(ErrorMessage = "Please enter a valid Youtube link.")]
        public string YoutubeLink { get; set; }

        [StringLength(200, ErrorMessage = "Address cannot be longer than 200 characters.")]
        public string Address { get; set; }

        [StringLength(100, ErrorMessage = "Working hours cannot be longer than 100 characters.")]
        public string WorkingHours { get; set; }

        [Phone(ErrorMessage = "Please enter a valid contact number.")]
        public string ContactNumber { get; set; }

        public string Location { get; set; }

        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Mail { get; set; }
        [ValidateNever]
        public string MiddlePanner { get; set; }
        [ValidateNever]
        public string LastPanner { get; set; }
    }
}
