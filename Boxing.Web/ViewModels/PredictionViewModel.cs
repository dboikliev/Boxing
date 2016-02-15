using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Boxing.Web.ViewModels
{
    public class PredictionViewModel
    {
        [Range(1, 2)]
        public int PredictedWinner { get; set; }
        public int MatchId { get; set; }
        public int UserId { get; set; }

    }
}