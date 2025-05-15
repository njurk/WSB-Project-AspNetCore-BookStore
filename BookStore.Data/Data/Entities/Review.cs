using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Data.Data.Entities
{
    public class Review
    {
        [Key]
        public int IdReview { get; set; }

        [Required(ErrorMessage = "User is required.")]
        [Display(Name = "User")]
        [ForeignKey("User")]
        public int IdUser { get; set; }
        public User? User { get; set; }

        [Required(ErrorMessage = "Book is required.")]
        [Display(Name = "Book")]
        [ForeignKey("Book")]
        public int IdBook { get; set; }
        public Book? Book { get; set; }

        [Required(ErrorMessage = "Rating is required.")]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public required int Rating { get; set; }

        [Required(ErrorMessage = "Comment is required.")]
        [StringLength(1000, ErrorMessage = "Comment cannot be longer than 1000 characters.")]
        public required string Comment { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Date added")]
        public DateTime DateAdded { get; set; }
    }
}
