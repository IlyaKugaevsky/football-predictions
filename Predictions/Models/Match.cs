﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Predictions.Models
{
    public class Match
    {
        //this comment is just for fun!

        public int MatchId { get; set; }
        public string Title { get; set; }
        public string Score { get; set; }
        public int Tour { get; set; }
        [Column(TypeName = "DateTime2")]
        public DateTime Date { get; set; }

        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
    }
}