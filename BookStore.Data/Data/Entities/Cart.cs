using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data.Data.Entities
{
    public class Cart
    {
        [Key]
        public int IdCart { get; set; }

        [Required]
        [ForeignKey("User")]
        [DisplayName("User")]
        public required int IdUser { get; set; }
        public User? User { get; set; }

        [Required]
        [ForeignKey("Book")]
        [DisplayName("Book")]
        public required int IdBook { get; set; }
        public Book? Book { get; set; }

        [Required]
        public required int Quantity { get; set; }

        [Required]
        public required decimal UnitPrice { get; set; }

    }
}
