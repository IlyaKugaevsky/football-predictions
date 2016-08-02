using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;

namespace Predictions.Models
{
    public class Match
    {
        public int MatchId { get; set; }
        public string Title { get; set; }
        public string Score { get; set; }
        public int Tour { get; set; }
        public DateTime Date { get; set; }

        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
    }
}