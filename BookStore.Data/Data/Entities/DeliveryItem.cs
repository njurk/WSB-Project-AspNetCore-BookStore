using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data.Data.Entities
{
    public class DeliveryItem
    {
        [Key]
        public int DeliveryItemID { get; set; }

        public required int DeliveryID { get; set; }

        [ForeignKey("DeliveryID")]
        public Delivery? Delivery { get; set; }

        public required int BookID { get; set; }

        [ForeignKey("BookID")]
        public Book? Book { get; set; }

        public required int Quantity { get; set; }
    }
}
