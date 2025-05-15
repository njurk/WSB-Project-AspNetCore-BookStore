using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BookStore.Data.Data.Entities
{
    public class User
    {
        [Key]
        public int IdUser { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters.")]
        public required string Username { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [DisplayName("First Name")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        [DisplayName("Last Name")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "Street is required.")]
        [StringLength(100)]
        public required string Street { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [StringLength(50)]
        public required string City { get; set; }

        [Required(ErrorMessage = "Postal code is required.")]
        public required string PostalCode { get; set; }

        [Required]
        [StringLength(20)]
        public required string Role { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<Collection> Collections { get; set; } = new List<Collection>();
        public ICollection<Cart> Carts { get; set; } = new List<Cart>();
    }
}
