using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Dominos.Models
{
    public class VmOrder
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int PaymentId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string PharmcistName { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int StockQuentity { get; set; }
        public string ProdcutsClassificationName { get; set; }
        // start columne secuirty
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
        // end columne secuirty
    }
}
