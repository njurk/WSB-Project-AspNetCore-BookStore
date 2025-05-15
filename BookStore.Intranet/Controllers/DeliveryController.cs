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
    public class DeliveryController : Controller
    {
        private readonly BookStoreContext _context;

        public DeliveryController(BookStoreContext context)
        {
            _context = context;
        }

        // GET: Delivery
        public async Task<IActionResult> Index()
        {
            var bookStoreContext = _context.Deliveries.Include(d => d.Supplier);
            return View(await bookStoreContext.ToListAsync());
        }

        // GET: Delivery/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delivery = await _context.Deliveries
                .Include(d => d.Supplier)
                .FirstOrDefaultAsync(m => m.IdDelivery == id);
            if (delivery == null)
            {
                return NotFound();
            }

            return View(delivery);
        }

        // GET: Delivery/Create
        public IActionResult Create()
        {
            ViewData["IdSupplier"] = new SelectList(_context.Suppliers, "IdSupplier", "Name");
            return View();
        }

        // POST: Delivery/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDelivery,IdSupplier,DeliveryDate")] Delivery delivery)
        {
            if (ModelState.IsValid)
            {
                _context.Add(delivery);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdSupplier"] = new SelectList(_context.Suppliers, "IdSupplier", "Name", delivery.IdSupplier);
            return View(delivery);
        }

        // GET: Delivery/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delivery = await _context.Deliveries.FindAsync(id);
            if (delivery == null)
            {
                return NotFound();
            }
            ViewData["IdSupplier"] = new SelectList(_context.Suppliers, "IdSupplier", "Name", delivery.IdSupplier);
            return View(delivery);
        }

        // POST: Delivery/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDelivery,IdSupplier,DeliveryDate")] Delivery delivery)
        {
            if (id != delivery.IdDelivery)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(delivery);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeliveryExists(delivery.IdDelivery))
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
            ViewData["IdSupplier"] = new SelectList(_context.Suppliers, "IdSupplier", "Name", delivery.IdSupplier);
            return View(delivery);
        }

        // GET: Delivery/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delivery = await _context.Deliveries
                .Include(d => d.Supplier)
                .FirstOrDefaultAsync(m => m.IdDelivery == id);
            if (delivery == null)
            {
                return NotFound();
            }

            return View(delivery);
        }

        // POST: Delivery/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var delivery = await _context.Deliveries.FindAsync(id);
            if (delivery != null)
            {
                _context.Deliveries.Remove(delivery);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeliveryExists(int id)
        {
            return _context.Deliveries.Any(e => e.IdDelivery == id);
        }
    }
}
