using BookStore.Data.Data;
using BookStore.Intranet.Services;
using BookStore.Intranet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BookStore.Intranet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BookStoreContext _context;
        private OrderService _orderService;

        public HomeController(ILogger<HomeController> logger, BookStoreContext context, OrderService orderService)
        {
            _logger = logger;
            _context = context;
            _orderService = orderService;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Index()
        {
            var recentOrders = await _orderService.GetOrdersWithTotalPriceAsync();

            var model = new DashboardViewModel
            {
                TotalBooks = await _context.Book.CountAsync(),
                TotalOrders = await _context.Order.CountAsync(),
                TotalDeliveries = await _context.Delivery.CountAsync(),
                RecentOrders = recentOrders
            };

            model.CalculateTotalRevenue();

            return View(model);
        }

    }
}
