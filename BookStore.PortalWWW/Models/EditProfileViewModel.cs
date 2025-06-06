using System.ComponentModel.DataAnnotations;

namespace BookStore.PortalWWW.Models
{
    public class EditProfileViewModel
    {
        public int IdUser { get; set; }

        [Display(Name = "Username")]
        public string Username { get; set; } = string.Empty;

        [Required, StringLength(50)]
        [Display(Name = "First name")]
        public string FirstName { get; set; } = string.Empty;

        [Required, StringLength(50)]
        [Display(Name = "Last name")]
        public string LastName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [StringLength(100)]
        public string Street { get; set; } = string.Empty;

        [StringLength(50)]
        public string City { get; set; } = string.Empty;

        [StringLength(20)]
        [Display(Name = "Postal code")]
        public string PostalCode { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "New password (optional)")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
        public string? NewPassword { get; set; }
    }
}
