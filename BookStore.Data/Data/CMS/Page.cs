using BookStore.Data.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data.Data.CMS
{
    public class Page
    {
        [Key]
        public int IdPage { get; set; }

        [Required]
        [MaxLength(15, ErrorMessage = "Link title's length can't exceed 15 characters")]
        [DisplayName("Link title")]
        public required string LinkTitle { get; set; }

        [Required]
        [MaxLength(15, ErrorMessage = "Page title's length can't exceed 15 characters")]
        [DisplayName("Page title")]
        public required string PageTitle { get; set; }

        [Required]
        public int Position { get; set; }

        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
