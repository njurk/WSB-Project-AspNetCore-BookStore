using Microsoft.AspNetCore.Mvc;
using BookStore.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using BookStore.Data.Data;

namespace BookStore.PortalWWW.ViewComponents
{
    public class GenresListViewComponent : ViewComponent
    {
        private readonly BookStoreContext _context;

        public GenresListViewComponent(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var genres = await _context.Genres
                .OrderBy(g => g.Name)
                .ToListAsync();
            return View(genres);
        }
    }
}
