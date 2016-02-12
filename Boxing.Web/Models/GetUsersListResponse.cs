using System.Collections.Generic;
using Boxing.Web.ViewModels;

namespace Boxing.Web.Models
{
    public class GetUsersListResponse
    {
        public int Total { get; set; }
        public int Skipped { get; set; }
        public IEnumerable<UserViewModel> Users { get; set; }
        public bool IsAscending { get; set; }
        public string SortedBy { get; set; }
    }
}