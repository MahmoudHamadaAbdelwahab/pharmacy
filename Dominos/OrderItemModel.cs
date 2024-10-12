using System.ComponentModel.DataAnnotations;

namespace Dominos.Models
{
    public class OrderItemModel
    {
        [Key]
        public int OrderItemId { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }

        public int ProductsId { get; set; }
        public ProductsModel TbProducts { get; set; }

        public int OrderId { get; set; }
        public OrderModel TbOrder { get; set; }
    }
}
