using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Boxing.Web.ViewModels
{
    public class ListViewModel<T>
    {
        public bool IsAscending { get; set; }
        public string SortedBy { get; set; }
        public int Total { get; set; }
        public int Skipped { get; set; }
        public int ItemsPerPage { get; set; } = 5;
        public int CurrentPage => Math.Min(Total, Skipped) / ItemsPerPage;
        public int PagesCount => (int)Math.Ceiling((double)Total / ItemsPerPage);
        public IEnumerable<T> Items { get; set; }
    }
}