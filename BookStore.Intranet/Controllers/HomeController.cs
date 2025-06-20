using BookStore.Data.Data;
using BookStore.Intranet.Services;
using BookStore.Intranet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace BookStore.Intranet.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BookStoreContext _context;
        private readonly OrderService _orderService;

        public HomeController(ILogger<HomeController> logger, BookStoreContext context, OrderService orderService)
        {
            _logger = logger;
            _context = context;
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            var now = DateTime.Now;
            var firstDayOfMonth = new DateTime(now.Year, now.Month, 1);

            var ordersThisMonth = await _context.Orders
                .Where(o => o.OrderDate >= firstDayOfMonth)
                .Include(o => o.OrderStatus)
                .Include(o => o.OrderItems)
                .ToListAsync();

            var recentOrders = ordersThisMonth
                .OrderByDescending(o => o.OrderDate)
                .Take(5)
                .Select(o => new OrderViewModel
                {
                    OrderID = o.IdOrder,
                    OrderDate = o.OrderDate,
                    OrderStatusName = o.OrderStatus.Name,
                    TotalPrice = o.OrderItems.Sum(oi => oi.Quantity * oi.UnitPrice)
                }).ToList();

            var dailyRevenue = Enumerable.Range(1, DateTime.DaysInMonth(now.Year, now.Month))
                .Select(day =>
                {
                    var date = new DateTime(now.Year, now.Month, day);
                    var total = ordersThisMonth
                        .Where(o => o.OrderDate.Day == day)
                        .SelectMany(o => o.OrderItems)
                        .Sum(oi => oi.Quantity * oi.UnitPrice);
                    return new { Day = day.ToString(), Revenue = total };
                })
                .ToList();

            var model = new DashboardViewModel
            {
                TotalBooks = await _context.Books.SumAsync(b => b.Quantity),
                TotalOrders = ordersThisMonth.Count,
                RecentOrders = recentOrders,
                ChartLabels = dailyRevenue.Select(d => d.Day).ToList(),
                ChartData = dailyRevenue.Select(d => d.Revenue).ToList()
            };

            model.CalculateTotalRevenue();

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
