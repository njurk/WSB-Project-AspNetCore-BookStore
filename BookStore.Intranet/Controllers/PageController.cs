using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStore.Data.Data;
using BookStore.Data.Data.CMS;

namespace BookStore.Intranet.Controllers
{
    public class PageController : Controller
    {
        private readonly BookStoreContext _context;

        public PageController(BookStoreContext context)
        {
            _context = context;
        }

        // GET: Page
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pages.ToListAsync());
        }

        // GET: Page/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = await _context.Pages
                .FirstOrDefaultAsync(m => m.IdPage == id);
            if (page == null)
            {
                return NotFound();
            }

            return View(page);
        }

        // GET: Page/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Page/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPage,LinkTitle,PageTitle,Position")] Page page)
        {
            if (ModelState.IsValid)
            {
                _context.Add(page);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(page);
        }

        // GET: Page/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = await _context.Pages.FindAsync(id);
            if (page == null)
            {
                return NotFound();
            }
            return View(page);
        }

        // POST: Page/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPage,LinkTitle,PageTitle,Position")] Page page)
        {
            if (id != page.IdPage)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(page);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PageExists(page.IdPage))
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
            return View(page);
        }

        // GET: Page/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = await _context.Pages
                .FirstOrDefaultAsync(m => m.IdPage == id);
            if (page == null)
            {
                return NotFound();
            }

            return View(page);
        }

        // POST: Page/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var page = _context.Pages.Find(id);
            if (page == null)
                return NotFound();

            _context.Pages.Remove(page);
            _context.SaveChanges();
            return Ok();
        }

        private bool PageExists(int id)
        {
            return _context.Pages.Any(e => e.IdPage == id);
        }
    }
}
