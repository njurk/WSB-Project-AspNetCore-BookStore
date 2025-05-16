using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BookStore.Data.Data;
using BookStore.Data.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BookStore.PortalWWW.Controllers
{
    [Authorize]
    public class CollectionController : Controller
    {
        private readonly BookStoreContext _context;

        public CollectionController(BookStoreContext context)
        {
            _context = context;
        }

        // GET: /Collection/
        public async Task<IActionResult> Index()
        {
            var username = User.Identity?.Name;
            if (string.IsNullOrEmpty(username))
                return Challenge();

            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
                return Unauthorized();

            var collections = await _context.Collections
                .Where(c => c.IdUser == user.IdUser)
                .Include(c => c.Book)
                .AsNoTracking()
                .ToListAsync();

            return View(collections);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int bookId)
        {
            var username = User.Identity?.Name;
            if (string.IsNullOrEmpty(username))
                return Challenge();

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
                return Unauthorized();

            bool exists = await _context.Collections.AnyAsync(c => c.IdUser == user.IdUser && c.IdBook == bookId);
            if (!exists)
            {
                var collectionItem = new Collection
                {
                    IdUser = user.IdUser,
                    IdBook = bookId
                };
                _context.Collections.Add(collectionItem);
                await _context.SaveChangesAsync();
            }

            var referer = Request.Headers["Referer"].ToString();
            if (!string.IsNullOrEmpty(referer))
                return Redirect(referer);

            return RedirectToAction("Index", "Books");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Toggle(int bookId)
        {
            var username = User.Identity?.Name;
            if (string.IsNullOrEmpty(username))
                return Unauthorized();

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
                return Unauthorized();

            var existing = await _context.Collections.FirstOrDefaultAsync(c => c.IdUser == user.IdUser && c.IdBook == bookId);

            bool added;
            if (existing == null)
            {
                var collectionItem = new Collection { IdUser = user.IdUser, IdBook = bookId };
                _context.Collections.Add(collectionItem);
                added = true;
            }
            else
            {
                _context.Collections.Remove(existing);
                added = false;
            }

            await _context.SaveChangesAsync();

            return Json(new { added });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> IsInCollection(int bookId)
        {
            var username = User.Identity?.Name;
            if (string.IsNullOrEmpty(username))
                return Json(new { isInCollection = false });

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
                return Json(new { isInCollection = false });

            bool exists = await _context.Collections.AnyAsync(c => c.IdUser == user.IdUser && c.IdBook == bookId);

            return Json(new { isInCollection = exists });
        }

    }
}
