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

        public IActionResult Index()
        {
            var now = DateTime.Now;
            var firstDayOfMonth = new DateTime(now.Year, now.Month, 1);

            var recentOrders = _context.Orders
                .Include(o => o.OrderStatus)
                .Include(o => o.OrderItems)
                .Where(o => o.OrderDate >= firstDayOfMonth)
                .OrderByDescending(o => o.OrderDate)
                .Take(5)
                .Select(o => new OrderViewModel
                {
                    OrderID = o.IdOrder,
                    OrderDate = o.OrderDate,
                    OrderStatusName = o.OrderStatus.Name,
                    TotalPrice = o.OrderItems.Sum(oi => oi.Quantity * oi.UnitPrice)
                }).ToList();

            var totalDeliveries = _context.Deliveries
                .Where(d => d.DeliveryDate >= firstDayOfMonth)
                .Count();

            var model = new DashboardViewModel
            {
                TotalBooks = _context.Books.Sum(b => b.Quantity),
                TotalOrders = _context.Orders.Count(o => o.OrderDate >= firstDayOfMonth),
                TotalDeliveries = totalDeliveries,
                RecentOrders = recentOrders
            };

            model.CalculateTotalRevenue();

            return View(model);

        }
    }
}
