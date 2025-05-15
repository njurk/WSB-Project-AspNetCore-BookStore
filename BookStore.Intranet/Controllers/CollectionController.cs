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
    public class CollectionController : Controller
    {
        private readonly BookStoreContext _context;

        public CollectionController(BookStoreContext context)
        {
            _context = context;
        }

        // GET: Collection
        public async Task<IActionResult> Index()
        {
            var bookStoreContext = _context.Collections.Include(c => c.Book).Include(c => c.User);
            return View(await bookStoreContext.ToListAsync());
        }

        // GET: Collection/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collection = await _context.Collections
                .Include(c => c.Book)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.IdCollection == id);
            if (collection == null)
            {
                return NotFound();
            }

            return View(collection);
        }

        // GET: Collection/Create
        public IActionResult Create()
        {
            ViewData["IdBook"] = new SelectList(_context.Books, "IdBook", "Title");
            ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "Username");
            return View();
        }

        // POST: Collection/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCollection,IdUser,IdBook")] Collection collection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(collection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdBook"] = new SelectList(_context.Books, "IdBook", "Title", collection.IdBook);
            ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "Username", collection.IdUser);
            return View(collection);
        }

        // GET: Collection/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collection = await _context.Collections.FindAsync(id);
            if (collection == null)
            {
                return NotFound();
            }
            ViewData["IdBook"] = new SelectList(_context.Books, "IdBook", "Title", collection.IdBook);
            ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "Username", collection.IdUser);
            return View(collection);
        }

        // POST: Collection/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCollection,IdUser,IdBook")] Collection collection)
        {
            if (id != collection.IdCollection)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(collection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CollectionExists(collection.IdCollection))
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
            ViewData["IdBook"] = new SelectList(_context.Books, "IdBook", "Title", collection.IdBook);
            ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "Username", collection.IdUser);
            return View(collection);
        }

        // GET: Collection/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collection = await _context.Collections
                .Include(c => c.Book)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.IdCollection == id);
            if (collection == null)
            {
                return NotFound();
            }

            return View(collection);
        }

        // POST: Collection/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var collection = await _context.Collections.FindAsync(id);
            if (collection != null)
            {
                _context.Collections.Remove(collection);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CollectionExists(int id)
        {
            return _context.Collections.Any(e => e.IdCollection == id);
        }
    }
}
