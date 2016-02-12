using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Boxing.Api.Services.Models
{
    public class UsersListModel
    {
        public int Total { get; set; }
        public int Skipped { get; set; }
        public IEnumerable<UserModel> Users { get; set; }
    }
}