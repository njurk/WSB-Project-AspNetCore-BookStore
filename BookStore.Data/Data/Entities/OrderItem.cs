using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BookStore.Data.Data.Entities
{
    public class OrderItem
    {
        [Key]
        public int IdOrderItem { get; set; }

        [Required(ErrorMessage = "Order is required.")]
        [DisplayName("Order")]
        [ForeignKey("Order")]
        public int IdOrder { get; set; }
        public Order? Order { get; set; }

        [Required(ErrorMessage = "Book is required.")]
        [DisplayName("Book")]
        [ForeignKey("Book")]
        public int IdBook { get; set; }
        public Book? Book { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Unit price is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Unit price must be at least 0.")]
        [DisplayName("Unit Price")]
        public decimal UnitPrice { get; set; }
    }
}
