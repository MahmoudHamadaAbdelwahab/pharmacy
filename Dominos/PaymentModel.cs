using System.ComponentModel.DataAnnotations;
namespace Dominos.Models
{
    public class PaymentModel
    {
        [Key]
        public int PaymentId { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public int Amount { get; set; }
        public int TransactionId { get; set; }

        public OrderModel TbOrder { get; set; }
    }
}
