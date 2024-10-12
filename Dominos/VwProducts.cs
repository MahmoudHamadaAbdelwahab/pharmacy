using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Dominos.Models
{
    public partial class VwProducts
    {
        [ValidateNever]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        //public string Images { get; set; }
        //public string Description { get; set; }
        //public decimal Price { get; set; }
        public int StockQuentity { get; set; }
        //public int SupplierId { get; set; }
        [ValidateNever]
        //public int PharmcistId { get; set; }
        //public int ProdcutsClassificationId { get; set; }
        public string ProdcutsClassificationName { get; set; }
        public string PharmcistName { get; set; }
        public string SupplierName { get; set; }
        //public int OrderItemId { get; set; }
        //public int Quantity { get; set; }

        public decimal PurchasePrice { get; set; }
        public decimal SalesPrice { get; set; }
        // start columne secuirty
        //[ValidateNever]
        //public string CreatedBy { get; set; } = null!;
        //[ValidateNever]
        //public DateTime CreatedDate { get; set; }
        //[ValidateNever]
        //public int CurrentState { get; set; }
        //[ValidateNever]
        //public string? UpdatedBy { get; set; }
        //[ValidateNever]
        //public DateTime? UpdatedDate { get; set; }
        // end columne secuirty
    }

}
