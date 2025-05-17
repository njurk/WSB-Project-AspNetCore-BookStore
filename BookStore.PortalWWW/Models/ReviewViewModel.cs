using System.ComponentModel.DataAnnotations;

namespace BookStore.PortalWWW.Models
{
    public class ReviewViewModel
    {
        public string BookTitle { get; set; }

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
