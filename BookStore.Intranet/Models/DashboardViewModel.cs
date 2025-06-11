namespace BookStore.Intranet.Models
{
    public class DashboardViewModel
    {
        public int TotalBooks { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }

        public List<string> ChartLabels { get; set; }
        public List<decimal> ChartData { get; set; }
        public List<OrderViewModel> RecentOrders { get; set; }

        public void CalculateTotalRevenue()
        {
            TotalRevenue = RecentOrders?.Sum(order => order.TotalPrice) ?? 0;
        }
    }
}
