using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStore.Data.Data;
using BookStore.Data.Data.Entities;
using Microsoft.AspNetCore.Authorization;

namespace BookStore.Intranet.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly BookStoreContext _context;

        public OrderController(BookStoreContext context)
        {
            _context = context;
        }

        // GET: Order
        public async Task<IActionResult> Index(string sortOrder)
        {
            var orders = _context.Orders
                .Include(o => o.OrderStatus)
                .Include(o => o.OrderItems)
                .Include(o => o.User)
                .AsQueryable();

            orders = sortOrder switch
            {
                "date_asc" => orders.OrderBy(o => o.OrderDate),
                "date_desc" or null => orders.OrderByDescending(o => o.OrderDate),
                "status" => orders.OrderBy(o => o.IdOrderStatus),
                _ => orders.OrderByDescending(o => o.OrderDate)
            };

            ViewBag.SortOrder = sortOrder;
            return View(await orders.ToListAsync());
        }

        // GET: Order/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.OrderStatus)
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Book)
                .FirstOrDefaultAsync(m => m.IdOrder == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Order/Create
        [HttpGet]
        public IActionResult Create()
        {
            var users = _context.Users.ToList();

            ViewData["IdOrderStatus"] = new SelectList(_context.OrderStatuses, "Id", "Name");
            ViewData["IdUser"] = new SelectList(users, "IdUser", "Username");
            ViewBag.Books = _context.Books.Select(b => new SelectListItem
            {
                Value = b.IdBook.ToString(),
                Text = b.Title
            }).ToList();

            var userAddresses = users.Select(u => new {
                u.IdUser,
                u.FirstName,
                u.LastName,
                u.Email,
                u.Street,
                u.City,
                u.PostalCode
            }).ToList();

            ViewBag.UserAddressesJson = System.Text.Json.JsonSerializer.Serialize(userAddresses);

            return View();
        }


        // POST: Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Order order, List<OrderItem> orderItems)
        {
            if (!ModelState.IsValid || orderItems == null || orderItems.Count == 0)
            {
                ModelState.AddModelError("", "At least 1 order item is required");
                ViewData["IdOrderStatus"] = new SelectList(_context.OrderStatuses, "Id", "Name", order.IdOrderStatus);
                ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "Username", order.IdUser);
                ViewBag.Books = _context.Books.Select(b => new SelectListItem { Value = b.IdBook.ToString(), Text = b.Title }).ToList();

                return View(order);
            }

            var bookList = await _context.Books
                .Where(b => orderItems.Select(i => i.IdBook).Contains(b.IdBook))
                .ToDictionaryAsync(b => b.IdBook);

            order.OrderItems = new List<OrderItem>();

            foreach (var item in orderItems)
            {
                if (!bookList.TryGetValue(item.IdBook, out var book))
                    continue;

                if (book.Quantity < item.Quantity)
                {
                    ModelState.AddModelError("", $"Not enough quantity for book: {book.Title}");
                    ViewData["IdOrderStatus"] = new SelectList(_context.OrderStatuses, "Id", "Name", order.IdOrderStatus);
                    ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "Username", order.IdUser);
                    ViewBag.Books = _context.Books.Select(b => new SelectListItem { Value = b.IdBook.ToString(), Text = b.Title }).ToList();
                    return View(order);
                }

                book.Quantity -= item.Quantity;

                item.UnitPrice = book.Price;
                order.OrderItems.Add(item);
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Order/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .Include(o => o.User)
                .FirstOrDefaultAsync(o => o.IdOrder == id);

            if (order == null)
                return NotFound();

            var users = _context.Users.ToList();

            ViewData["IdOrderStatus"] = new SelectList(_context.OrderStatuses, "Id", "Name", order.IdOrderStatus);
            ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "Username", order.IdUser);

            ViewBag.Books = _context.Books.Select(b => new SelectListItem
            {
                Value = b.IdBook.ToString(),
                Text = b.Title
            }).ToList();

            ViewBag.Address = order.User != null ? new
            {
                order.User.FirstName,
                order.User.LastName,
                order.User.Email,
                order.User.Street,
                order.User.City,
                order.User.PostalCode
            } : null;

            ViewBag.UserAddressesJson = System.Text.Json.JsonSerializer.Serialize(users.Select(u => new {
                u.IdUser,
                u.FirstName,
                u.LastName,
                u.Email,
                u.Street,
                u.City,
                u.PostalCode
            }));

            return View(order);
        }

        // POST: Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Order order, List<OrderItem> orderItems)
        {
            if (id != order.IdOrder)
                return NotFound();

            if (!ModelState.IsValid || orderItems == null || !orderItems.Any())
            {
                ModelState.AddModelError("", "At least 1 order item is required");

                ViewData["IdOrderStatus"] = new SelectList(_context.OrderStatuses, "Id", "Name", order.IdOrderStatus);
                ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "Username", order.IdUser);
                ViewBag.Books = _context.Books.Select(b => new SelectListItem
                {
                    Value = b.IdBook.ToString(),
                    Text = b.Title
                }).ToList();

                return View(order);
            }

            try
            {
                var existingOrder = await _context.Orders.FindAsync(order.IdOrder);
                if (existingOrder == null)
                    return NotFound();

                existingOrder.OrderDate = order.OrderDate;
                existingOrder.IdUser = order.IdUser;
                existingOrder.IdOrderStatus = order.IdOrderStatus;
                existingOrder.FirstName = order.FirstName;
                existingOrder.LastName = order.LastName;
                existingOrder.Email = order.Email;
                existingOrder.Street = order.Street;
                existingOrder.City = order.City;
                existingOrder.PostalCode = order.PostalCode;

                var existingItems = _context.OrderItems.Where(oi => oi.IdOrder == id);
                _context.OrderItems.RemoveRange(existingItems);

                var bookPrices = await _context.Books
                    .Where(b => orderItems.Select(i => i.IdBook).Contains(b.IdBook))
                    .ToDictionaryAsync(b => b.IdBook, b => b.Price);

                foreach (var item in orderItems)
                {
                    if (!bookPrices.TryGetValue(item.IdBook, out var price))
                        continue;

                    _context.OrderItems.Add(new OrderItem
                    {
                        IdOrder = id,
                        IdBook = item.IdBook,
                        Quantity = item.Quantity,
                        UnitPrice = price
                    });
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(order.IdOrder))
                    return NotFound();
                throw;
            }
        }


        // GET: Order/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.OrderStatus)
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.IdOrder == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Order/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.IdOrder == id);
        }
    }
}
