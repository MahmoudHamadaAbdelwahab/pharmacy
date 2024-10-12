using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Dominos.Models
{
    public partial class SliderModel
    {
        [Key]
        [ValidateNever]
        public int SliderId { get; set; }

        [MaxLength(100, ErrorMessage = "Title can't be longer than 100 characters.")]
        public string? Title { get; set; }

        [MaxLength(500, ErrorMessage = "Description can't be longer than 500 characters.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Image is required.")]
        public string ImageName { get; set; } = null!;

        [Range(0, 1, ErrorMessage = "Current state must be 0 (Inactive) or 1 (Active).")]
        public int CurrentState { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Created Date is required.")]
        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage = "Created By is required.")]
        public string CreatedBy { get; set; } = null!;

        [DataType(DataType.Date)]
        public DateTime? UpdatedDate { get; set; }
        [ValidateNever]
        public string? UpdatedBy { get; set; }
    }
}
