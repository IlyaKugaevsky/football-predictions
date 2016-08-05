using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Predictions.Models;

namespace Predictions.ViewModels
{
    public class MatchesViewModel
    {
        public MatchesViewModel (List<Team> teamlist, List<Match> matchlist)
        {
            this.Teamlist = teamlist;
            this.Matchlist = matchlist;
        }

        public List<Team> Teamlist { get; set; }
        public List<Match> Matchlist { get; set; }
    }
}