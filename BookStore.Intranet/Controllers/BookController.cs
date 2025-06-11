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
    public class BookController : Controller
    {
        private readonly BookStoreContext _context;

        public BookController(BookStoreContext context)
        {
            _context = context;
        }

        // GET: Book
        public async Task<IActionResult> Index()
        {
            var bookStoreContext = _context.Books.Include(b => b.Author).OrderByDescending(b => b.IdBook);
            return View(await bookStoreContext.ToListAsync());
        }

        // GET: Book/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(m => m.IdBook == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Book/Create
        public IActionResult Create()
        {
            var authors = _context.Authors
                .Select(a => new
                {
                    Id = a.IdAuthor,
                    FullName = a.FirstName + " " + a.LastName
                })
                .ToList();

            ViewBag.IdAuthor = new SelectList(authors, "Id", "FullName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book, IFormFile ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                    Directory.CreateDirectory(uploadsFolder);

                    var fileName = Path.GetFileName(ImageFile.FileName);
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }

                    book.ImageUrl = fileName;
                }

                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.IdAuthor = new SelectList(_context.Authors, "Id", "Name", book.IdAuthor);
            return View(book);
        }
        
        // GET: Book/Edit/5
        public IActionResult Edit(int id)
        {
            var book = _context.Books.Include(b => b.Author).FirstOrDefault(b => b.IdBook == id);

            if (book == null)
            {
                return NotFound();
            }

            var authors = _context.Authors
                .Select(a => new
                {
                    Id = a.IdAuthor,
                    FullName = a.FirstName + " " + a.LastName
                })
                .ToList();

            ViewBag.IdAuthor = new SelectList(authors, "Id", "FullName", book.IdAuthor);
            return View(book);
        }


        // POST: Book/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdBook,Title,Description,Price,NumberOfPages,YearPublished,Language,Quantity,ISBN,IdAuthor,ImageUrl")] Book book)
        {
            if (id != book.IdBook)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.IdBook))
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
            ViewData["IdAuthor"] = new SelectList(_context.Authors, "IdAuthor", "FirstName", book.IdAuthor);
            return View(book);
        }

        // GET: Book/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(m => m.IdBook == id);

            if (book == null)
            {
                return NotFound();
            }

            var authorFullName = book.Author != null ? book.Author.FirstName + " " + book.Author.LastName : "Unknown Author";
            ViewBag.AuthorFullName = authorFullName;

            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.IdBook == id);
        }
    }
}
