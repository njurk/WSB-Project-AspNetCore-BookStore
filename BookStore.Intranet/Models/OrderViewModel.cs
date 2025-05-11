namespace BookStore.Intranet.Models
{
    public class OrderViewModel
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }

        public string OrderStatusName { get; set; }
    }

}
