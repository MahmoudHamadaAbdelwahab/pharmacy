using System.ComponentModel.DataAnnotations;

namespace Dominos.Models
{
    public class OrderModel
    {
        [Key]
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int TotalAmount { get; set; }

        public int PaymentId { get; set; }
        public PaymentModel TbPayment { get; set; }

        public int CustomerId { get; set; }
        public CustomerModel TbCustomer { get; set; }

        public ICollection<OrderItemModel> TbOrderItem { get; set; }

    }
}
