using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Data.Data.Entities
{
    public class Delivery
    {
        [Key]
        public int IdDelivery { get; set; }

        [Required(ErrorMessage = "Supplier is required.")]
        [Display(Name = "Supplier")]
        [ForeignKey("Supplier")]
        public int IdSupplier { get; set; }

        public Supplier? Supplier { get; set; }

        [Required(ErrorMessage = "Delivery date is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Delivery Date")]
        public DateTime DeliveryDate { get; set; }

        public ICollection<DeliveryItem> DeliveryItems { get; set; } = new List<DeliveryItem>();
    }
}
