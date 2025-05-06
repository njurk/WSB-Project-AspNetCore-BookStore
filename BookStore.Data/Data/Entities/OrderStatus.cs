using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data.Data.Entities
{
    public class OrderStatus
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Status name cannot be longer than 50 characters.")]
        [Display(Name = "Status Name")]
        public required string Name { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
