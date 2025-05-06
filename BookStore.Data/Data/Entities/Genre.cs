using System.ComponentModel.DataAnnotations;

namespace BookStore.Data.Data.Entities
{
    public class Genre
    {
        [Key]
        public int IdGenre { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        [Display(Name = "Genre Name")]
        public required string Name { get; set; }

        public ICollection<BookGenre> BookGenres { get; set; } = new List<BookGenre>();
    }
}
