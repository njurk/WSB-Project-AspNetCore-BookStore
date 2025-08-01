﻿using BookStore.Data.Data.Entities;
using BookStore.Data.Data.CMS;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data.Data
{
    public class BookStoreContext : DbContext
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; } = default!;
        public DbSet<Author> Authors { get; set; } = default!;
        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Collection> Collections { get; set; } = default!;
        public DbSet<Cart> Carts { get; set; } = default!;
        public DbSet<Order> Orders { get; set; } = default!;
        public DbSet<OrderItem> OrderItems { get; set; } = default!;
        public DbSet<Genre> Genres { get; set; } = default!;
        public DbSet<BookGenre> BookGenres { get; set; } = default!;
        public DbSet<Review> Reviews { get; set; } = default!;
        public DbSet<OrderStatus> OrderStatuses { get; set; } = default!;
        public DbSet<Page> Pages { get; set; } = default!;
        public DbSet<Post> Posts { get; set; } = default!;
        public DbSet<StaticText> StaticTexts { get; set; }

    }
}

