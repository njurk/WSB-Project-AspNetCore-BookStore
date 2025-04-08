using BookStore.Data.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data.Data
{
    public class BookStoreContext : DbContext
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options)
            : base(options)
        {
        }

        public DbSet<Author> Author { get; set; } = default!;
        public DbSet<Book> Book { get; set; } = default!;
        public DbSet<Customer> Customer { get; set; } = default!;
        public DbSet<Delivery> Delivery { get; set; } = default!;
        public DbSet<DeliveryItem> DeliveryItem { get; set; } = default!;
        public DbSet<Genre> Genre { get; set; } = default!;
        public DbSet<Order> Order { get; set; } = default!;
        public DbSet<OrderItem> OrderItem { get; set; } = default!;
        public DbSet<Supplier> Supplier { get; set; } = default!;
    }
}
