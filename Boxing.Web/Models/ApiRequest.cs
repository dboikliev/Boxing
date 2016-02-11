using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestTestWebApp.Models
{
    public class ApiRequest
    {
        public string EndPoint { get; set; }

        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
        public object Request { get; set; }
    }
}