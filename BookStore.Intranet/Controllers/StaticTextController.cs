using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStore.Data.Data;
using BookStore.Data.Data.CMS;

namespace BookStore.Intranet.Controllers
{
    public class StaticTextController : Controller
    {
        private readonly BookStoreContext _context;

        public StaticTextController(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.StaticTexts.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var staticText = await _context.StaticTexts.FirstOrDefaultAsync(m => m.Id == id);
            if (staticText == null)
                return NotFound();

            return View(staticText);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var staticText = await _context.StaticTexts.FindAsync(id);
            if (staticText == null)
                return NotFound();

            return View(staticText);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Key,Value")] StaticText staticText)
        {
            if (id != staticText.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Attach(staticText).Property(x => x.Value).IsModified = true;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(staticText);
        }
    }
}
