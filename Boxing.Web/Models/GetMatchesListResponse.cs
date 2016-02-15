using System.Collections.Generic;
using Boxing.Web.ViewModels;

namespace Boxing.Web.Models
{
    public class GetMatchesListResponse
    {
        public int Total { get; set; }
        public int Skipped { get; set; }
        public IEnumerable<MatchViewModel> Matches { get; set; }
    }
}