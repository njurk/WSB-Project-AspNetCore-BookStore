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
    public class BookGenreController : Controller
    {
        private readonly BookStoreContext _context;

        public BookGenreController(BookStoreContext context)
        {
            _context = context;
        }

        // GET: BookGenre
        public async Task<IActionResult> Index()
        {
            var bookStoreContext = _context.BookGenres.Include(b => b.Book).Include(b => b.Genre);
            return View(await bookStoreContext.ToListAsync());
        }

        // GET: BookGenre/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookGenre = await _context.BookGenres
                .Include(b => b.Book)
                .Include(b => b.Genre)
                .FirstOrDefaultAsync(m => m.IdBookGenre == id);
            if (bookGenre == null)
            {
                return NotFound();
            }

            return View(bookGenre);
        }

        // GET: BookGenre/Create
        public IActionResult Create()
        {
            ViewData["IdBook"] = new SelectList(_context.Books, "IdBook", "Description");
            ViewData["IdGenre"] = new SelectList(_context.Genres, "IdGenre", "Name");
            return View();
        }

        // POST: BookGenre/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdBookGenre,IdBook,IdGenre")] BookGenre bookGenre)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookGenre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdBook"] = new SelectList(_context.Books, "IdBook", "Description", bookGenre.IdBook);
            ViewData["IdGenre"] = new SelectList(_context.Genres, "IdGenre", "Name", bookGenre.IdGenre);
            return View(bookGenre);
        }

        // GET: BookGenre/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookGenre = await _context.BookGenres.FindAsync(id);
            if (bookGenre == null)
            {
                return NotFound();
            }
            ViewData["IdBook"] = new SelectList(_context.Books, "IdBook", "Description", bookGenre.IdBook);
            ViewData["IdGenre"] = new SelectList(_context.Genres, "IdGenre", "Name", bookGenre.IdGenre);
            return View(bookGenre);
        }

        // POST: BookGenre/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdBookGenre,IdBook,IdGenre")] BookGenre bookGenre)
        {
            if (id != bookGenre.IdBookGenre)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookGenre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookGenreExists(bookGenre.IdBookGenre))
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
            ViewData["IdBook"] = new SelectList(_context.Books, "IdBook", "Description", bookGenre.IdBook);
            ViewData["IdGenre"] = new SelectList(_context.Genres, "IdGenre", "Name", bookGenre.IdGenre);
            return View(bookGenre);
        }

        // GET: BookGenre/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookGenre = await _context.BookGenres
                .Include(b => b.Book)
                .Include(b => b.Genre)
                .FirstOrDefaultAsync(m => m.IdBookGenre == id);
            if (bookGenre == null)
            {
                return NotFound();
            }

            return View(bookGenre);
        }

        // POST: BookGenre/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookGenre = await _context.BookGenres.FindAsync(id);
            if (bookGenre != null)
            {
                _context.BookGenres.Remove(bookGenre);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookGenreExists(int id)
        {
            return _context.BookGenres.Any(e => e.IdBookGenre == id);
        }
    }
}
