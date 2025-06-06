namespace BookStore.PortalWWW.Models
{
    public class OrderDetailsViewModel
    {
        public int OrderId { get; set; }
        public DateTime DatePlaced { get; set; }
        public string Status { get; set; }
        public decimal TotalPrice { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }

        public List<OrderItemViewModel> Items { get; set; }
    }
}
