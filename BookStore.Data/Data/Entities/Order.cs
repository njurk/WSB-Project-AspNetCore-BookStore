using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Data.Data.Entities
{
    public class Order
    {
        [Key]
        public int IdOrder { get; set; }

        [Required(ErrorMessage = "User is required.")]
        [Display(Name = "User")]
        [ForeignKey("User")]
        public int IdUser { get; set; }
        public User? User { get; set; }

        [Required(ErrorMessage = "Order date is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Order Date")]
        public required DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Order status is required.")]
        [Display(Name = "Order Status")]
        [ForeignKey("OrderStatus")]
        public int IdOrderStatus { get; set; }
        public OrderStatus? OrderStatus { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
