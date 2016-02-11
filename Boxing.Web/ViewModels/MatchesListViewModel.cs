using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Boxing.Web.ViewModels
{
    public class MatchesListViewModel
    {
        public int Total { get; set; }
        public int Skipped { get; set; }

        public int MatchesPerPage { get; set; } = 5;

        public int CurrentPage => Math.Min(Total, Skipped) / MatchesPerPage;
        public int PagesCount => (int)Math.Ceiling((double)Total / MatchesPerPage);
        public IEnumerable<MatchViewModel> Matches { get; set; }
    }
}