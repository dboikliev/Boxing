using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Web;
using Boxing.Contracts;

namespace Boxing.Api.Services.Models
{
    public class LoginModel
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Username { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; }
        
        public string AuthenticationToken { get; set; }
        public RolesEnum Role { get; set; }
    }
}