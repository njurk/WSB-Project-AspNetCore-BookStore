using System.Collections.Generic;
using BookStore.Data.Data.Entities;

namespace BookStore.Intranet.Models
{
    public class BookDetailsViewModel
    {
        public Book Book { get; set; }
        public List<string> Genres { get; set; } = new();
    }
}
