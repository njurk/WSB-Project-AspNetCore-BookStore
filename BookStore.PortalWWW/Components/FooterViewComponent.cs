using BookStore.PortalWWW.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.PortalWWW.Components
{
    public class FooterViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var model = new FooterViewModel();
            return View(model);
        }
    }
}
