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
    public class CartController : Controller
    {
        private readonly BookStoreContext _context;

        public CartController(BookStoreContext context)
        {
            _context = context;
        }

        // GET: Cart
        public async Task<IActionResult> Index()
        {
            var bookStoreContext = _context.Carts.Include(c => c.Book).Include(c => c.User);
            return View(await bookStoreContext.ToListAsync());
        }

        // GET: Cart/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts
                .Include(c => c.Book)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.IdCart == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // GET: Cart/Create
        public IActionResult Create()
        {
            ViewData["IdBook"] = new SelectList(_context.Books, "IdBook", "Title");
            ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "Username");
            return View();
        }

        // POST: Cart/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCart,IdUser,IdBook,Quantity")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                var book = await _context.Books.FindAsync(cart.IdBook);
                if (book == null)
                {
                    ModelState.AddModelError("IdBook", "Selected book does not exist.");
                    ViewData["IdBook"] = new SelectList(_context.Books, "IdBook", "Title", cart.IdBook);
                    ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "Username", cart.IdUser);
                    return View(cart);
                }
                cart.UnitPrice = book.Price;
                _context.Add(cart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdBook"] = new SelectList(_context.Books, "IdBook", "Title", cart.IdBook);
            ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "Username", cart.IdUser);
            return View(cart);
        }


        // GET: Cart/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            ViewData["IdBook"] = new SelectList(_context.Books, "IdBook", "Title", cart.IdBook);
            ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "Username", cart.IdUser);
            return View(cart);
        }

        // POST: Cart/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCart,IdUser,IdBook,Quantity,UnitPrice")] Cart cart)
        {
            if (id != cart.IdCart)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartExists(cart.IdCart))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdBook"] = new SelectList(_context.Books, "IdBook", "Title", cart.IdBook);
            ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "Username", cart.IdUser);
            return View(cart);
        }

        // GET: Cart/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts
                .Include(c => c.Book)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.IdCart == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Cart/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cart = await _context.Carts.FindAsync(id);
            if (cart != null)
            {
                _context.Carts.Remove(cart);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartExists(int id)
        {
            return _context.Carts.Any(e => e.IdCart == id);
        }
    }
}
