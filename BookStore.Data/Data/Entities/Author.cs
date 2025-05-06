using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Data.Data.Entities
{
    public class Author
    {
        [Key]
        public int IdAuthor { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [DisplayName("First Name")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        [DisplayName("Last Name")]
        public required string LastName { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
