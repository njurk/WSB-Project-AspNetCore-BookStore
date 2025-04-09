using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BookStore.Data.Data.Entities
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        [Required(ErrorMessage = "Customer is required.")]
        [Display(Name="Customer")]
        public required int CustomerID { get; set; }

        [ForeignKey("CustomerID")]
        public Customer? Customer { get; set; }

        [Display(Name = "Order date")]
        [Required(ErrorMessage = "Order date is required.")]
        public required DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public required string Status { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
