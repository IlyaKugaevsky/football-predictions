using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Predictions.Models;
using System.Web.Mvc;

namespace Predictions.ViewModels
{
    public class MatchesViewModel
    {
        public List<Team> Teamlist { get; set; }
        public List<Match> Matchlist { get; set; }

        public Match NewMatch { get; set; }

    }
}