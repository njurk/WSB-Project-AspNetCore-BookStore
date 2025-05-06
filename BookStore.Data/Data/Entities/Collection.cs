using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data.Data.Entities
{
    public class Collection
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "User is required")]
        [Display(Name = "User")]
        [ForeignKey("User")]
        public int IdUser { get; set; }
        public User? User { get; set; }

        [Required(ErrorMessage = "Book is required")]
        [Display(Name = "Book")]
        [ForeignKey("Book")]
        public int IdBook { get; set; }
        public Book? Book { get; set; }
    }
}
