using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Boxing.Api.Services.Models
{
    public class PredictionModel
    {
        public int Id { get; set; }
        public int PredictedWinner { get; set; }
        public int MatchId { get; set; }
        public int UserId { get; set; }
    }
}