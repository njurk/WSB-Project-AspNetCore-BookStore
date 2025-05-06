using System.ComponentModel.DataAnnotations;

namespace BookStore.Data.Data.Entities
{
    public class Supplier
    {
        [Key]
        public int IdSupplier { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        [Display(Name = "Supplier Name")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Phone is required.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        public required string Phone { get; set; }

        public ICollection<Delivery> Deliveries { get; set; } = new List<Delivery>();
    }
}
