using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Printing;
using System.Linq;
using System.Web;

namespace Boxing.Web.Models
{
    public class LoginViewModel
    {
        [Required(AllowEmptyStrings = false)]
        public string Username { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; }
    }
}