﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Boxing.Web.ViewModels
{
    public class UserViewModel
    {
        public int? UserId { get; set; }
      
        public string Username { get; set; }

        public string FullName { get; set; }


        public double? Rating { get; set; }
    }
}