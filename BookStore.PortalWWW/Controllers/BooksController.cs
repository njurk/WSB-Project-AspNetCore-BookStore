using BookStore.Data.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace BookStore.PortalWWW.Controllers
{
    public class BooksController : Controller
    {
        private readonly ILogger<BooksController> _logger;
        private readonly BookStoreContext _context;

        public BooksController(ILogger<BooksController> logger, BookStoreContext context)
        {
            _logger = logger;
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IActionResult Index()
        {
            var books = _context.Books.ToList();
            return View(books);
        }

        public IActionResult Details(int id)
        {
            var book = _context.Books
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
