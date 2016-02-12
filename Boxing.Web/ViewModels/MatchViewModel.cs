using System;
using System.ComponentModel.DataAnnotations;
using Boxing.Web.Constants;
using Boxing.Web.Models;

namespace Boxing.Web.ViewModels
{
    public class MatchViewModel
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        [MaxLength(50)]
        public string Boxer1 { get; set; }
        [MaxLength(50)]
        [Required(AllowEmptyStrings = false)]
        public string Boxer2 { get; set; }
        [MaxLength(100)]
        [Required(AllowEmptyStrings = false)]
        public string Place { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateOfMatch { get; set; }
        [Required(AllowEmptyStrings = false)]
        [MaxLength(500)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Range(1, 2)]
        public int? Winner { get; set; }

        public int? PredictedWinner { get; set; }

        public MatchStatusesEnum Status { get; set; }
    }
}