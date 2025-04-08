using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Data.Data.Entities
{
    public class OrderItem
    {
        [Key]
        public int OrderItemID { get; set; }

        public required int OrderID { get; set; }

        [ForeignKey("OrderID")]
        public Order? Order { get; set; }

        public required int BookID { get; set; }

        [ForeignKey("BookID")]
        public Book? Book { get; set; }

        public required int Quantity { get; set; }
    }
}
