using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data.Data.Entities
{
    public class Supplier
    {
        [Key]
        public int SupplierID { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public required string Name { get; set; }

        [DisplayName("Phone number")]
        [Required(ErrorMessage = "Phone number is required.")]
        public required string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Street is required.")]
        public required string Street { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public required string City { get; set; }

        [DisplayName("Postal Code")]
        [Required(ErrorMessage = "Postal code is required.")]
        public required string PostalCode { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        public required string Country { get; set; }

        public ICollection<Delivery> Deliveries { get; set; } = new List<Delivery>();
    }
}
