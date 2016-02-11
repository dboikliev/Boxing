using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Boxing.Api.Services.Models
{
    public class MatchesListModel
    {
        public int Total { get; set; }
        public int Skipped { get; set; }
        public IEnumerable<MatchModel> Matches { get; set; } 
    }
}