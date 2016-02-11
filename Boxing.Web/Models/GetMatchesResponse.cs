using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Boxing.Web.ViewModels;

namespace Boxing.Web.Models
{
    public class GetMatchesResponse
    {
        public int Total { get; set; }
        public int Skipped { get; set; }
        public IEnumerable<MatchViewModel> Matches { get; set; }
    }
}