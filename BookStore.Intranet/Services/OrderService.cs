using BookStore.Data.Data;
using BookStore.Intranet.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Intranet.Services
{
    public class OrderService
    {
        private readonly BookStoreContext _context;

        public OrderService(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<List<OrderViewModel>> GetOrdersWithTotalPriceAsync()
        {
            return await _context.Orders
                .Include(order => order.OrderStatus)
                .Select(order => new OrderViewModel
                {
                    OrderID = order.IdOrder,
                    OrderDate = order.OrderDate,
                    TotalPrice = order.OrderItems.Sum(item => item.Quantity * (item.Book != null ? item.Book.Price : 0))
                })
                .ToListAsync();
        }
    }
}
