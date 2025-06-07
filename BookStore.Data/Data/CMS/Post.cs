using BookStore.Data.Data.CMS;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Data.Data.CMS
{
    public class Post
    {
        [Key]
        public int IdPost { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Post title's length can't exceed 100 characters")]
        [DisplayName("Post title")]
        public required string Title { get; set; }

        [Required]
        public required string Introduction { get; set; }

        [Required]
        public required string Content { get; set; }

        public string? ImageUrl { get; set; }

        [Required]
        [DisplayName("Date added")]
        public DateTime DateAdded { get; set; }

        [Required]
        public int Position { get; set; }

        [Required]
        public int PageId { get; set; }

        public Page? Page { get; set; }
    }
}
