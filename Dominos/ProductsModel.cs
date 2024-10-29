using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Dominos.Models
{
    public class ProductsModel
    {

        [Key]
        [ValidateNever] 
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Please enter the product name.")]
        [StringLength(100, ErrorMessage = "Product name cannot be longer than 100 characters.")]
        public string ProductName { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter the Sales Price.")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a valid Sales Price.")]
        public int SalesPrice { get; set; }

        [Required(ErrorMessage = "Please enter the Purchase Price.")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a valid Purchase Price.")]
        public int PurchasePrice { get; set; }

        [Required(ErrorMessage = "Please enter the stock quantity.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid stock quantity.")]
        public int StockQuentity { get; set; }

        // start columne secuirty
        [ValidateNever]
        public string CreatedBy { get; set; } = null!;
        [ValidateNever]
        public DateTime CreatedDate { get; set; }
        [ValidateNever]
        public int CurrentState { get; set; }
        [ValidateNever]
        public string UpdatedBy { get; set; }
        [ValidateNever]
        public DateTime UpdatedDate { get; set; }
        // end columne secuirty

        // One-to-One relationship with Supplier
        [Required(ErrorMessage = "Please select a supplier.")]
        public int SupplierId { get; set; }

        [ValidateNever] // Navigation property, not part of form input
        public SupplierModel TbSupplier { get; set; }

        // Many-to-One relationship with Pharmacist
        [Required(ErrorMessage = "Please select a pharmacist.")]
        public int PharmcistId { get; set; }

        [ValidateNever] // Navigation property
        public PharmcistsModel TbPharmcists { get; set; }

        // one-to-one relationship with OrderItemModel
        [ValidateNever] // Navigation property
        public OrderItemModel TbOrderItem { get; set; }

        // Many-to-One relationship with Product Classification
        [Required(ErrorMessage = "Please select a product classification.")]
        public int ProdcutsClassificationId { get; set; }

        [ValidateNever] // Navigation property
        public ProdcutsClassificationModel TbProdcutsClassification { get; set; }
        public virtual ICollection<ImagesModel> TbImages { get; set; }

    }
}
