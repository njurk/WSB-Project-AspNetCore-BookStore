using BookStore.Data.Data;
using BookStore.PortalWWW.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.PortalWWW.Components
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly BookStoreContext _context;

        public FooterViewComponent(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var texts = await _context.StaticTexts
            .ToDictionaryAsync(t => t.Key, t => t.Value);

            return View("Default", texts);
        }
    }
}
