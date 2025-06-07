using BookStore.Data.Data.CMS;
using BookStore.Data.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

public class PageLinksViewComponent : ViewComponent
{
    private readonly BookStoreContext _context;

    public PageLinksViewComponent(BookStoreContext context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var pages = await _context.Pages
            .OrderBy(p => p.Position)
            .ToListAsync();

        return View(pages);
    }
}
