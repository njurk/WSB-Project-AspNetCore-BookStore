using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BookStore.Data.Data;
using BookStore.Data.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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

        public async Task<IActionResult> Index()
        {
            int userId = GetCurrentUserId();
            if (userId == 0) return Challenge();

            var cartItems = await _context.Carts
                .Where(c => c.IdUser == userId)
                .Include(c => c.Book)
                .ToListAsync();

            return View(cartItems);
        }

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            int userId = GetCurrentUserId();
            if (userId == 0)
                return RedirectToAction("Login", "Account");

            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return RedirectToAction("Login", "Account");

            var cartItems = await _context.Carts
                .Include(c => c.Book)
                .Where(c => c.IdUser == userId)
                .ToListAsync();

            if (!cartItems.Any())
            {
                TempData["Message"] = "Cart is empty";
                return RedirectToAction("Index");
            }

            ViewBag.CartItems = cartItems;
            ViewBag.TotalItems = cartItems.Sum(i => i.Quantity);
            ViewBag.TotalPrice = cartItems.Sum(i => i.Quantity * i.UnitPrice);

            var model = new CheckoutFormModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Street = user.Street,
                City = user.City,
                PostalCode = user.PostalCode
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(CheckoutFormModel form)
        {
            int userId = GetCurrentUserId();
            if (userId == 0)
                return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid)
            {
                var cartItems = await _context.Carts
                    .Include(c => c.Book)
                    .Where(c => c.IdUser == userId)
                    .ToListAsync();

                ViewBag.CartItems = cartItems;
                ViewBag.TotalItems = cartItems.Sum(i => i.Quantity);
                ViewBag.TotalPrice = cartItems.Sum(i => i.Quantity * i.UnitPrice);

                return View(form);
            }

            var cartItemsToOrder = await _context.Carts
                .Include(c => c.Book)
                .Where(c => c.IdUser == userId)
                .ToListAsync();

            if (!cartItemsToOrder.Any())
            {
                TempData["Message"] = "Cart is empty";
                return RedirectToAction("Index");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var order = new Order
                {
                    IdUser = userId,
                    OrderDate = DateTime.UtcNow,
                    IdOrderStatus = 1,
                    FirstName = form.FirstName,
                    LastName = form.LastName,
                    Email = form.Email,
                    Street = form.Street,
                    City = form.City,
                    PostalCode = form.PostalCode
                };

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                foreach (var cartItem in cartItemsToOrder)
                {
                    var orderItem = new OrderItem
                    {
                        IdOrder = order.IdOrder,
                        IdBook = cartItem.IdBook,
                        Quantity = cartItem.Quantity,
                        UnitPrice = cartItem.UnitPrice
                    };
                    _context.OrderItems.Add(orderItem);
                }

                await _context.SaveChangesAsync();

                _context.Carts.RemoveRange(cartItemsToOrder);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                TempData["SuccessMessage"] = "Order placed successfully!";

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                var cartItems = await _context.Carts
                    .Include(c => c.Book)
                    .Where(c => c.IdUser == userId)
                    .ToListAsync();

                ViewBag.CartItems = cartItems;
                ViewBag.TotalItems = cartItems.Sum(i => i.Quantity);
                ViewBag.TotalPrice = cartItems.Sum(i => i.Quantity * i.UnitPrice);

                ModelState.AddModelError("", "Error placing order: " + ex.Message);

                return View(form);
            }
        }

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
                    UnitPrice = book.Price
                };
                _context.Carts.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += quantity;
            }

            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Book added to cart!" });
        }

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

            return Ok();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(int cartId)
        {
            var cartItem = await _context.Carts.FindAsync(cartId);
            if (cartItem == null)
                return NotFound();

            _context.Carts.Remove(cartItem);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            return userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId) ? userId : 0;
        }
    }
}
