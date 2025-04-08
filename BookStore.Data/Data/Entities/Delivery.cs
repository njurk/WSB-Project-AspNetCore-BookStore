using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data.Data.Entities
{
    public class Delivery
    {
        [Key]
        public int DeliveryID { get; set; }

        [Display(Name = "Delivery date")]
        [Required(ErrorMessage = "Delivery date is required.")]
        public required DateTime DeliveryDate { get; set; }

        [Display(Name = "Supplier")]
        [Required(ErrorMessage = "Supplier is required.")]
        public required int SupplierID { get; set; }

        [ForeignKey("SupplierID")]
        public Supplier? Supplier { get; set; }

        public ICollection<DeliveryItem> DeliveryItems { get; set; } = new List<DeliveryItem>();
    }
}
