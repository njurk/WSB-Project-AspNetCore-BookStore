using BookStore.Data.Data;
using BookStore.Data.Data.CMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BookStore.PortalWWW.Controllers
{
    [AllowAnonymous]
    public class PagesController : Controller
    {
        private readonly BookStoreContext _context;
        public PagesController(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
                return NotFound();

            var page = await _context.Pages
                .Include(p => p.Posts.OrderByDescending(post => post.DateAdded))
                .FirstOrDefaultAsync(p => p.IdPage == id);

            if (page == null)
                return NotFound();

            return View(page);
        }

        public async Task<IActionResult> Post(int? id)
        {
            if (id == null)
                return NotFound();

            var post = await _context.Posts
                .Include(p => p.Page)
                .FirstOrDefaultAsync(p => p.IdPost == id);

            if (post == null)
                return NotFound();

            return View(post);
        }
    }
}
