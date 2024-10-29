using Microsoft.AspNetCore.Mvc.Rendering;

namespace Pharmacy_Website.Models
{
    public class SearchModel
    {
        public string? ProductName { get; set; }
        public int? ProdcutsClassificationId { get; set; }
        public int? SupplierId { get; set; }
        public int? PharmcistId { get; set; }

        // For Dropdown Filters
        public IEnumerable<SelectListItem>? ProductClassifications { get; set; }
        public IEnumerable<SelectListItem>? Suppliers { get; set; }
        public IEnumerable<SelectListItem>? Pharmcists { get; set; }
    }
}
