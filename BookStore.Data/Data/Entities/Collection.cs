using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data.Data.Entities
{
    public class Collection
    {
        [Key]
        public int IdCollection { get; set; }

        [Required]
        [ForeignKey("User")]
        public required int IdUser { get; set; }
        public User? User { get; set; }

        [Required]
        [ForeignKey("Book")]
        public required int IdBook { get; set; }
        public Book? Book { get; set; }
    }
}
