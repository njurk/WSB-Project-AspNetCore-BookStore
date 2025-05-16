using BookStore.Data.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace BookStore.PortalWWW.Controllers
{
    [AllowAnonymous]
    public class BooksController : Controller
    {
        private readonly ILogger<BooksController> _logger;
        private readonly BookStoreContext _context;

        public BooksController(ILogger<BooksController> logger, BookStoreContext context)
        {
            _logger = logger;
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IActionResult Index(string? searchTerm)
        {
            var query = _context.Books
                .Include(b => b.Author)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                string lowerSearchTerm = searchTerm.ToLower();

                query = query.Where(b =>
                    b.Title.ToLower().Contains(lowerSearchTerm) ||
                    (b.Author != null &&
                     (b.Author.FirstName.ToLower() + " " + b.Author.LastName.ToLower()).Contains(lowerSearchTerm))
                );
            }

            var books = query.ToList();

            return View(books);
        }

        public IActionResult Details(int id)
        {
            var book = _context.Books
                .Include(b => b.Author)
                .Include(b => b.BookGenres)
                    .ThenInclude(bg => bg.Genre)
                .Include(b => b.Reviews)
                    .ThenInclude(r => r.User)
                .FirstOrDefault(b => b.IdBook == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }
    }
}
