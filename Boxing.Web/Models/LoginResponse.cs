using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Boxing.Web.Models
{
    public class LoginResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string AuthenticationToken { get; set; }

        public RolesEnum Role { get; set; }
    }
}