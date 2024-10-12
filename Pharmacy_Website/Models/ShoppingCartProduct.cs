namespace Pharmacy_Website.Models
{
    public class ShoppingCartProduct
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageName { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
    }
}
