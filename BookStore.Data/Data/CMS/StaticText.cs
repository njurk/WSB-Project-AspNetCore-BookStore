using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data.Data.CMS
{
    public class StaticText
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Key { get; set; }

        public string? Value { get; set; }
    }
}
