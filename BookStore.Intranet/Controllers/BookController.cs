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
using BookStore.Intranet.Models;

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
        public async Task<IActionResult> Index(string query, string sortOrder)
        {
            ViewData["SearchQuery"] = query;
            ViewData["SortOrder"] = sortOrder;

            var booksQuery = _context.Books.Include(b => b.Author).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query))
            {
                var tokens = query.Trim().ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                foreach (var token in tokens)
                {
                    booksQuery = booksQuery.Where(b =>
                        b.Title.ToLower().Contains(token) ||
                        b.Author.FirstName.ToLower().Contains(token) ||
                        b.Author.LastName.ToLower().Contains(token) ||
                        b.ISBN.ToLower().Contains(token)
                    );
                }
            }

            booksQuery = sortOrder switch
            {
                "title" => booksQuery.OrderBy(b => b.Title),
                "oldest" => booksQuery.OrderBy(b => b.IdBook),
                "quantity_asc" => booksQuery.OrderBy(b => b.Quantity),
                "quantity_desc" => booksQuery.OrderByDescending(b => b.Quantity),
                "price_asc" => booksQuery.OrderBy(b => b.Price),
                "price_desc" => booksQuery.OrderByDescending(b => b.Price),
                "year_asc" => booksQuery.OrderBy(b => b.YearPublished),
                "year_desc" => booksQuery.OrderByDescending(b => b.YearPublished),
                "pages_asc" => booksQuery.OrderBy(b => b.NumberOfPages),
                "pages_desc" => booksQuery.OrderByDescending(b => b.NumberOfPages),
                _ => booksQuery.OrderByDescending(b => b.IdBook)
            };

            var model = await booksQuery.ToListAsync();
            return View(model);
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

            var genres = await _context.BookGenres
                .Where(bg => bg.IdBook == id)
                .Include(bg => bg.Genre)
                .Select(bg => bg.Genre!.Name)
                .ToListAsync();

            var viewModel = new BookDetailsViewModel
            {
                Book = book,
                Genres = genres
            };

            return View(viewModel);
        }

        // GET: Book/Create
        [HttpGet]
        public IActionResult Create()
        {
            var authors = _context.Authors
                .Select(a => new
                {
                    Id = a.IdAuthor,
                    FullName = a.FirstName + " " + a.LastName
                })
                .ToList();

            ViewBag.GenreList = new MultiSelectList(_context.Genres, "IdGenre", "Name");
            ViewBag.IdAuthor = new SelectList(authors, "Id", "FullName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book, List<int> selectedGenres, IFormFile ImageFile)
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

                if (selectedGenres != null && selectedGenres.Any())
                {
                    foreach (var genreId in selectedGenres)
                    {
                        _context.BookGenres.Add(new BookGenre
                        {
                            IdBook = book.IdBook,
                            IdGenre = genreId
                        });
                    }

                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            var authors = _context.Authors
                .Select(a => new
                {
                    Id = a.IdAuthor,
                    FullName = a.FirstName + " " + a.LastName
                })
                .ToList();

            ViewBag.IdAuthor = new SelectList(authors, "Id", "FullName", book.IdAuthor);

            var genres = _context.Genres?.ToList() ?? new List<Genre>();
            selectedGenres = selectedGenres ?? new List<int>();

            ViewBag.GenreList = new MultiSelectList(genres, "IdGenre", "Name", selectedGenres);

            return View(book);
        }

        // GET: Book/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound();

            var selectedGenres = await _context.BookGenres
                .Where(bg => bg.IdBook == id)
                .Select(bg => bg.IdGenre)
                .ToListAsync();

            await PopulateSelectLists(book.IdAuthor, selectedGenres);
            return View(book);
        }

        // POST: Book/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Book/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Book book, List<int> selectedGenres, IFormFile? ImageFile)
        {
            if (id != book.IdBook) return NotFound();

            if (!ModelState.IsValid)
            {
                await PopulateSelectLists(book.IdAuthor, selectedGenres);
                return View(book);
            }

            if (ImageFile != null && ImageFile.Length > 0)
            {
                var fileName = Path.GetFileName(ImageFile.FileName);
                var uploadsPath = Path.Combine("wwwroot/images", fileName);
                Directory.CreateDirectory(Path.GetDirectoryName(uploadsPath)!);
                using var stream = new FileStream(uploadsPath, FileMode.Create);
                await ImageFile.CopyToAsync(stream);
                book.ImageUrl = fileName;
            }

            try
            {
                _context.Update(book);
                var existingGenres = _context.BookGenres.Where(bg => bg.IdBook == id);
                _context.BookGenres.RemoveRange(existingGenres);

                if (selectedGenres != null)
                {
                    var newGenres = selectedGenres.Select(genreId => new BookGenre
                    {
                        IdBook = book.IdBook,
                        IdGenre = genreId
                    });

                    _context.BookGenres.AddRange(newGenres);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(book.IdBook)) return NotFound();
                throw;
            }
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
        [HttpPost("Book/DeleteConfirmed/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound();

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.IdBook == id);
        }
        private async Task PopulateSelectLists(int? selectedAuthorId = null, List<int>? selectedGenres = null)
        {
            ViewBag.IdAuthor = new SelectList(
                await _context.Authors
                    .Select(a => new { a.IdAuthor, FullName = a.FirstName + " " + a.LastName })
                    .ToListAsync(),
                "IdAuthor", "FullName", selectedAuthorId
            );

            ViewBag.GenreList = new MultiSelectList(
                await _context.Genres.ToListAsync(),
                "IdGenre", "Name", selectedGenres
            );
        }
    }
}
