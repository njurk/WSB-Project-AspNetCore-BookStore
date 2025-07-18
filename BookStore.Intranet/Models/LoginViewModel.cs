﻿using System.ComponentModel.DataAnnotations;

namespace BookStore.Intranet.Models
{
    public class LoginViewModel
    {
        [Required]
        public required string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }

}
