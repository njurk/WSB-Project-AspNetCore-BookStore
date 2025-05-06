using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Data.Data.Entities
{
    public class BookGenre
    {
        [Key]
        public int IdBookGenre { get; set; }

        [DisplayName("Book")]
        [Required(ErrorMessage = "Book is required.")]
        [ForeignKey("Book")]
        public int IdBook { get; set; }
        public Book? Book { get; set; }

        [DisplayName("Genre")]
        [Required(ErrorMessage = "Genre is required.")]
        [ForeignKey("Genre")]
        public int IdGenre { get; set; }
        public Genre? Genre { get; set; }
    }
}
