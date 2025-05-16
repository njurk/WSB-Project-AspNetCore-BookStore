using System.ComponentModel.DataAnnotations;

namespace BookStore.PortalWWW.Models
{
    public class EditReviewViewModel
    {
        public int IdReview { get; set; }
        public int BookId { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        public string Comment { get; set; }
    }

}
