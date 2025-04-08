using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BookStore.Data.Data.Entities
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "Customer's name is required.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Customer's surname is required.")]
        public required string Surname { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        public required string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Street is required.")]
        public required string Street { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public required string City { get; set; }

        [DisplayName("Postal Code")]
        [Required(ErrorMessage = "Postal code is required.")]
        public required string PostalCode { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        public required string Country { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
