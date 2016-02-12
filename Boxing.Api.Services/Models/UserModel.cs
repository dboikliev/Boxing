using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Boxing.Api.Services.Models
{
    public class UserModel
    {
        public int? UserId { get; set; }
        [Required(AllowEmptyStrings = false)]
        [MaxLength(50)]
        public string Username { get; set; }
        
        [Required(AllowEmptyStrings = false)]
        [MaxLength(50)]
        public string FullName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; }

        [Compare(nameof(Password))]
        public string RepeatedPassword { get; set; }

        public double? Rating { get; set; }
    }
}