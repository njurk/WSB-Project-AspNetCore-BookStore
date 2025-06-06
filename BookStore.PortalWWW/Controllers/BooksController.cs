using BookStore.Data.Data;
using BookStore.Data.Data.Entities;
using BookStore.PortalWWW.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;

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

        public async Task<IActionResult> Details(int id)
        {
            var book = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.BookGenres).ThenInclude(bg => bg.Genre)
                .Include(b => b.Reviews).ThenInclude(r => r.User)
                .FirstOrDefaultAsync(b => b.IdBook == id);

            if (book == null) return NotFound();

            return View(book);
        }

        public async Task<IActionResult> ByGenre(int genreId)
        {
            var booksQuery = _context.Books
                .Include(b => b.Author)
                .Include(b => b.BookGenres).ThenInclude(bg => bg.Genre)
                .Where(b => b.BookGenres.Any(bg => bg.IdGenre == genreId));

            var books = await booksQuery.ToListAsync();

            var genre = await _context.Genres.FindAsync(genreId);
            ViewBag.SelectedGenreName = genre?.Name;

            return View("Index", books);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> PostReview(int BookId, int Rating, string Comment, int? ReviewId)
        {
            if (Rating < 1 || Rating > 5 || string.IsNullOrWhiteSpace(Comment))
            {
                TempData["ReviewError"] = "Please provide a valid rating and comment.";
                return RedirectToAction("Details", new { id = BookId });
            }

            var userIdString = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
            {
                return Unauthorized();
            }

            if (ReviewId.HasValue)
            {
                var review = await _context.Reviews.FindAsync(ReviewId.Value);
                if (review == null || review.IdUser != userId)
                    return Forbid();

                review.Rating = Rating;
                review.Comment = Comment;
                _context.Reviews.Update(review);
            }
            else
            {
                var review = new Review
                {
                    IdBook = BookId,
                    IdUser = userId,
                    Rating = Rating,
                    Comment = Comment,
                    DateAdded = DateTime.UtcNow
                };
                _context.Reviews.Add(review);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = BookId });
        }

        // POST: Books/EditReview
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditReview(EditReviewViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var review = await _context.Reviews
                .FirstOrDefaultAsync(r => r.IdReview == model.IdReview);

            if (review == null)
                return NotFound();

            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdString == null || review.IdUser != int.Parse(userIdString))
                return Forbid();

            review.Rating = model.Rating;
            review.Comment = model.Comment;
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = review.IdBook });
        }

        // POST: Books/DeleteReview/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            var review = await _context.Reviews
                .FirstOrDefaultAsync(r => r.IdReview == reviewId);

            if (review == null)
                return NotFound();

            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdString == null || review.IdUser != int.Parse(userIdString))
                return Forbid();

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = review.IdBook });
        }
    }
}
