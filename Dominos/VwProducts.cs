using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominos.Models
{
    public partial class VwProducts
    {
        [Key]
        [ValidateNever]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int TbImages { get; set; }
        //public string Description { get; set; }
        //public decimal Price { get; set; }
        public int StockQuentity { get; set; }
        //public int SupplierId { get; set; }
        //[ValidateNever]
        //public int PharmcistId { get; set; }
        //public int ProdcutsClassificationId { get; set; }
        public string ProdcutsClassificationName { get; set; }
        public string PharmcistName { get; set; }
        public string SupplierName { get; set; }
        //public int OrderItemId { get; set; }
        //public int Quantity { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SalesPrice { get; set; }
    }

}
