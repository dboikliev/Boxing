using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxing.Core.DataAccess.Entities
{
    public class Prediction
    {
        public int Id { get; set; }
        public int PredictedWinner { get; set; }
        public int MatchId { get; set; }
        public Match Match { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } 
    }
}
