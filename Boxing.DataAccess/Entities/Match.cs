using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxing.Core.DataAccess.Entities
{
    public class Match
    {
        public int Id { get; set; }
        public string Boxer1 { get; set; }
        public string Boxer2 { get; set; }
        public string Place { get; set; }
        public DateTime DateOfMatch { get; set; }
        public string Description { get; set; }
        public int? Winner { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
        public ICollection<Prediction> Predictions { get; set; } 
    }
}
