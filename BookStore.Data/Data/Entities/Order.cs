using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Data.Data.Entities
{
    public class Order
    {
        [Key]
        public int IdOrder { get; set; }

        [Required(ErrorMessage = "User is required.")]
        [Display(Name = "User")]
        [ForeignKey("User")]
        public int IdUser { get; set; }
        public User? User { get; set; }

        [Required(ErrorMessage = "Order date is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Order date")]
        public required DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [DisplayName("First name")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        [DisplayName("Last name")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Street is required.")]
        [StringLength(100)]
        public required string Street { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [StringLength(50)]
        public required string City { get; set; }

        [Required(ErrorMessage = "Postal code is required.")]
        public required string PostalCode { get; set; }

        [Required(ErrorMessage = "Order status is required.")]
        [Display(Name = "Order status")]
        [ForeignKey("OrderStatus")]
        public int IdOrderStatus { get; set; }
        public OrderStatus? OrderStatus { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
