namespace BookStore.PortalWWW.Models
{
    public class OrderItemViewModel
    {
        public int IdBook { get; set; }
        public string BookTitle { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
