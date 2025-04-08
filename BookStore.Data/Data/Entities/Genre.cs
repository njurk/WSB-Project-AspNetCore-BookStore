using System.ComponentModel.DataAnnotations;

namespace BookStore.Data.Data.Entities
{
    public class Genre
    {
        [Key]
        public int GenreID { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public required string Name { get; set; }
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
