using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BookStore.Data.Data;
using BookStore.Data.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.PortalWWW.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly BookStoreContext _context;

        public CartController(BookStoreContext context)
        {
            _context = context;
        }

        // GET: /Cart/
        public async Task<IActionResult> Index()
        {
            var username = User.Identity?.Name;
            if (string.IsNullOrEmpty(username))
                return Challenge();

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
                return Unauthorized();

            var cartItems = await _context.Carts
                .Where(c => c.IdUser == user.IdUser)
                .Include(c => c.Book)
                .ToListAsync();

            return View(cartItems);
        }

        // POST: /Cart/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int bookId, int quantity = 1)
        {
            var username = User.Identity?.Name;
            if (string.IsNullOrEmpty(username))
                return Unauthorized();

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
                return Unauthorized();

            var book = await _context.Books.FindAsync(bookId);
            if (book == null)
                return NotFound();

            var cartItem = await _context.Carts
                .FirstOrDefaultAsync(c => c.IdUser == user.IdUser && c.IdBook == bookId);

            if (cartItem == null)
            {
                cartItem = new Cart
                {
                    IdUser = user.IdUser,
                    IdBook = bookId,
                    Quantity = quantity,
                    UnitPrice = (int)(book.Price * 100)
                };
                _context.Carts.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += quantity;
            }

            await _context.SaveChangesAsync();

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                // AJAX request - return JSON success
                return Json(new { success = true, message = "Book added to cart!" });
            }

            // Normal POST - redirect
            return RedirectToAction("Index");
        }

        // POST: /Cart/UpdateQuantity
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateQuantity(int cartId, int quantity)
        {
            if (quantity < 1)
                return BadRequest();

            var cartItem = await _context.Carts.FindAsync(cartId);
            if (cartItem == null)
                return NotFound();

            cartItem.Quantity = quantity;
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // POST: /Cart/Remove
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(int cartId)
        {
            var cartItem = await _context.Carts.FindAsync(cartId);
            if (cartItem == null)
                return NotFound();

            _context.Carts.Remove(cartItem);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
