using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStore.Data.Data;
using BookStore.Data.Data.Entities;

namespace BookStore.Intranet.Controllers
{
    public class DeliveryItemController : Controller
    {
        private readonly BookStoreContext _context;

        public DeliveryItemController(BookStoreContext context)
        {
            _context = context;
        }

        // GET: DeliveryItem
        public async Task<IActionResult> Index()
        {
            var bookStoreContext = _context.DeliveryItem.Include(d => d.Book).Include(d => d.Delivery);
            return View(await bookStoreContext.ToListAsync());
        }

        // GET: DeliveryItem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryItem = await _context.DeliveryItem
                .Include(d => d.Book)
                .Include(d => d.Delivery)
                .FirstOrDefaultAsync(m => m.DeliveryItemID == id);
            if (deliveryItem == null)
            {
                return NotFound();
            }

            return View(deliveryItem);
        }

        // GET: DeliveryItem/Create
        public IActionResult Create()
        {
            ViewData["BookID"] = new SelectList(_context.Book, "BookID", "Description");
            ViewData["DeliveryID"] = new SelectList(_context.Delivery, "DeliveryID", "DeliveryID");
            return View();
        }

        // POST: DeliveryItem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeliveryItemID,DeliveryID,BookID,Quantity")] DeliveryItem deliveryItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deliveryItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookID"] = new SelectList(_context.Book, "BookID", "Description", deliveryItem.BookID);
            ViewData["DeliveryID"] = new SelectList(_context.Delivery, "DeliveryID", "DeliveryID", deliveryItem.DeliveryID);
            return View(deliveryItem);
        }

        // GET: DeliveryItem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryItem = await _context.DeliveryItem.FindAsync(id);
            if (deliveryItem == null)
            {
                return NotFound();
            }
            ViewData["BookID"] = new SelectList(_context.Book, "BookID", "Description", deliveryItem.BookID);
            ViewData["DeliveryID"] = new SelectList(_context.Delivery, "DeliveryID", "DeliveryID", deliveryItem.DeliveryID);
            return View(deliveryItem);
        }

        // POST: DeliveryItem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DeliveryItemID,DeliveryID,BookID,Quantity")] DeliveryItem deliveryItem)
        {
            if (id != deliveryItem.DeliveryItemID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deliveryItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeliveryItemExists(deliveryItem.DeliveryItemID))
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
            ViewData["BookID"] = new SelectList(_context.Book, "BookID", "Description", deliveryItem.BookID);
            ViewData["DeliveryID"] = new SelectList(_context.Delivery, "DeliveryID", "DeliveryID", deliveryItem.DeliveryID);
            return View(deliveryItem);
        }

        // GET: DeliveryItem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryItem = await _context.DeliveryItem
                .Include(d => d.Book)
                .Include(d => d.Delivery)
                .FirstOrDefaultAsync(m => m.DeliveryItemID == id);
            if (deliveryItem == null)
            {
                return NotFound();
            }

            return View(deliveryItem);
        }

        // POST: DeliveryItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deliveryItem = await _context.DeliveryItem.FindAsync(id);
            if (deliveryItem != null)
            {
                _context.DeliveryItem.Remove(deliveryItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeliveryItemExists(int id)
        {
            return _context.DeliveryItem.Any(e => e.DeliveryItemID == id);
        }
    }
}
