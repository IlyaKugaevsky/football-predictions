using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Predictions.Models
{
    public class Match
    {
        public int MatchId { get; set; }
        public string Title { get; set; }
        public string Score { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "DateTime2")]
        public DateTime Date { get; set; }

        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }

        public int TourId { get; set; }
        public Tour Tour { get; set;  }
    }
}