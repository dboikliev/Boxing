using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxing.Contracts.Dto
{
    public class PredictionDto
    {
        public int Id { get; set; }
        public int PredictedWinner { get; set; }
        public int MatchId { get; set; }
        public int UserId { get; set; }
    }
}
