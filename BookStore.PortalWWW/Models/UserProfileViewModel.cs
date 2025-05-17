using System.ComponentModel.DataAnnotations;

namespace BookStore.PortalWWW.Models
{
    public class UserProfileViewModel
    {
        public int IdUser { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Username { get; set; } = null!;

        [Required]
        [StringLength(50)]
        [Display(Name = "First name")]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(50)]
        [Display(Name = "Last name")]
        public string LastName { get; set; } = null!;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string Street { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string City { get; set; } = null!;

        [Required]
        [Display(Name = "Postal code")]
        public string PostalCode { get; set; } = null!;

        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6)]
        [Display(Name = "New Password (optional)")]
        public string? Password { get; set; }

        public List<OrderViewModel> Orders { get; set; } = new();
        public List<EditReviewViewModel> Reviews { get; set; } = new();
    }
}
