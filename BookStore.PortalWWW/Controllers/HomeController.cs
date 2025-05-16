using System.Diagnostics;
using BookStore.Data.Data;
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

        public IActionResult Index()
        {
            var featuredBooks = _context.Books
                .Include(b => b.Author)
                .OrderByDescending(b => b.IdBook)
                .Take(6)
                .ToList();
            var popularBooks = _context.Books
                .Include(b => b.Author)
                .OrderBy(b => b.IdBook)
                .Take(6)
                .ToList();

            var model = new HomeViewModel
            {
                FeaturedBooks = featuredBooks,
                PopularBooks = popularBooks
            };

            return View(model);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
