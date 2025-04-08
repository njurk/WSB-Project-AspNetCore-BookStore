using System.ComponentModel.DataAnnotations;

namespace BookStore.Data.Data.Entities
{
    public class Author
    {
        [Key]
        public int AuthorID { get; set; }

        [Required(ErrorMessage = "Author's name is required.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Author's surname is required.")]
        public required string Surname { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
