using BookStore.Data.Data.CMS;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Data.Data.Entities
{
    public class Post
    {
        [Key]
        public int IdPost { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Post title's length can't exceed 100 characters")]
        [DisplayName("Post Title")]
        public required string Title { get; set; }

        [Required]
        [DisplayName("Content")]
        public required string Content { get; set; }

        [Required]
        [DisplayName("Date added")]
        public DateTime DateAdded { get; set; }

        [Required]
        [DisplayName("Position")]
        public int Position { get; set; }

        [Required]
        public int PageId { get; set; }

        public Page? Page { get; set; }
    }
}
