namespace Pharmacy_Website.Models
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            lstProducts = new List<ShoppingCartProduct>();
        }
        public List<ShoppingCartProduct> lstProducts { get; set; }
        public decimal TotalAllProd { get; set; }
        public string PromoCode { get; set; }
    }
}
