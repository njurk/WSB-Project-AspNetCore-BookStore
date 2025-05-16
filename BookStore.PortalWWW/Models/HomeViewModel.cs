using BookStore.Data.Data.Entities;
using System.Collections.Generic;

namespace BookStore.PortalWWW.Models
{
    public class HomeViewModel
    {
        public List<Book> FeaturedBooks { get; set; } = new();
        public List<Book> PopularBooks { get; set; } = new();
    }
}
