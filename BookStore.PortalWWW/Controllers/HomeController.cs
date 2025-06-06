using System.Diagnostics;
using BookStore.Data.Data;
using BookStore.Data.Data.Entities;
using BookStore.PortalWWW.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.PortalWWW.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BookStoreContext _context;

        public HomeController(ILogger<HomeController> logger, BookStoreContext context)
        {
            _logger = logger;
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IActionResult> Index()
        {
            var featuredBooks = await _context.Books
                .Include(b => b.Author)
                .OrderByDescending(b => b.IdBook)
                .Take(6)
                .ToListAsync();

            var popularBooksWithRatings = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Reviews)
                .Select(b => new BookWithAvgRatingViewModel
                {
                    Book = b,
                    AverageRating = b.Reviews.Any() ? b.Reviews.Average(r => r.Rating) : 0
                })
                .OrderByDescending(b => b.AverageRating)
                .Take(4)
                .ToListAsync();

            ViewData["FeaturedBooks"] = featuredBooks;
            ViewData["PopularBooksWithRatings"] = popularBooksWithRatings;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
