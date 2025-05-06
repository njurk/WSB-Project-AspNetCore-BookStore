using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Data.Data.Entities
{
    public class Book
    {
        [Key]
        public int IdBook { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public required string Title { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public required string Description { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Number of pages is required.")]
        [Range(1, int.MaxValue)]
        [DisplayName("Number of Pages")]
        public required int NumberOfPages { get; set; }

        [Required(ErrorMessage = "Year published is required.")]
        [Range(1, 9999, ErrorMessage = "Year published must be between 1 and 9999.")]
        [DisplayName("Year Published")]
        public required int YearPublished { get; set; }

        [Required(ErrorMessage = "Language is required.")]
        public required string Language { get; set; }

        [Required(ErrorMessage = "Quantity of at least 0 is required.")]
        [Range(0, int.MaxValue)]
        public required int Quantity { get; set; }

        [Required(ErrorMessage = "ISBN is required.")]
        [RegularExpression(@"^\d{10}$|^\d{13}$", 
            ErrorMessage = "Invalid ISBN format (10 or 13 digits).")]
        public required string ISBN { get; set; }

        [Required(ErrorMessage = "Author is required.")]
        [DisplayName("Author")]
        [ForeignKey("Author")]
        public int IdAuthor { get; set; }
        public Author? Author { get; set; }

        [DisplayName("Image")]
        public string ImageUrl { get; set; } = string.Empty;

        public ICollection<Collection> Collections { get; set; } = new List<Collection>();
        public ICollection<BookGenre> BookGenres { get; set; } = new List<BookGenre>();
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public ICollection<DeliveryItem> DeliveryItems { get; set; } = new List<DeliveryItem>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}