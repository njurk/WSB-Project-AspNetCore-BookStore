using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BookStore.Data.Data.Entities
{
    public class Book
    {
        [Key]
        public int BookID { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public required string Title { get; set; }

        [Required(ErrorMessage = "ISBN is required.")]
        public required string ISBN { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public required string Description { get; set; }

        [DisplayName("Year published")]
        [Required(ErrorMessage = "Year of publishing is required.")]
        public required int PublishedYear { get; set; }

        [DisplayName("Number of pages")]
        [Required(ErrorMessage = "Number of pages is required.")]
        public required int NumberOfPages { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Column(TypeName = "decimal(18,2)")]
        public required decimal Price { get; set; }

        [DisplayName("Image")]
        public string ImageUrl { get; set; } = string.Empty;

        [DisplayName("Author")]
        [Required(ErrorMessage = "Author is required.")]
        public required int AuthorID { get; set; }

        [ForeignKey("AuthorID")]
        public Author? Author { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public ICollection<DeliveryItem> DeliveryItems { get; set; } = new List<DeliveryItem>();
        public ICollection<Genre> Genres { get; set; } = new List<Genre>();
    }
}
