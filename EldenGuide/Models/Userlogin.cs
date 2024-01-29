﻿using System.ComponentModel.DataAnnotations;

namespace EldenGuide.Models
{
    public class Userlogin
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        //[StringLength]
        public string Password { get; set; }
    }
}
