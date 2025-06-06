using System.ComponentModel;

namespace BookStore.PortalWWW.Models
{
    public class UserProfileViewModel
    {
        public int IdUser { get; set; }
        public string Username { get; set; } = "";

        [DisplayName("First name")]
        public string FirstName { get; set; } = "";

        [DisplayName("Last name")]
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Street { get; set; } = "";
        public string City { get; set; } = "";

        [DisplayName("Postal code")]
        public string PostalCode { get; set; } = "";

        public List<OrderViewModel> Orders { get; set; } = new();
        public List<EditReviewViewModel> Reviews { get; set; } = new();
    }

}
