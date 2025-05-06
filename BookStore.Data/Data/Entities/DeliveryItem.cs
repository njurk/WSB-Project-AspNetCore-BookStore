using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Data.Data.Entities
{
    public class DeliveryItem
    {
        [Key]
        public int IdDeliveryItem { get; set; }

        [Required(ErrorMessage = "Delivery is required.")]
        [Display(Name = "Delivery")]
        [ForeignKey("Delivery")]
        public int IdDelivery { get; set; }

        public Delivery? Delivery { get; set; }

        [Required(ErrorMessage = "Book is required.")]
        [DisplayName("Book")]
        [ForeignKey("Book")]
        public required int IdBook { get; set; }

        public Book? Book { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
