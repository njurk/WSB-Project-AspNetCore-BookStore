using BookStore.Data.Data.Entities;

namespace BookStore.PortalWWW.Models
{
    public class BookWithAvgRatingViewModel
    {
        public Book Book { get; set; }
        public double AverageRating { get; set; }
    }
}
