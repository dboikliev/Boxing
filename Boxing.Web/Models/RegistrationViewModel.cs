using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Boxing.Web.Models
{
    public class RegistrationViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]

        public string Password { get; set; }
        [Compare(nameof(Password))]
        public string RepeatedPassword { get; set; }
    }
}