using System.ComponentModel.DataAnnotations;

namespace BookStore.PortalWWW.Models
{
    public class EditReviewViewModel : ReviewViewModel
    {
        public int IdReview { get; set; }
        public int BookId { get; set; }
    }

}
