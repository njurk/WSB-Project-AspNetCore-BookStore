namespace BookStore.PortalWWW.Models
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public DateTime DatePlaced { get; set; }
        public string Status { get; set; } = string.Empty;
        public int TotalItems { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
