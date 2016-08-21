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
        //display
        public List<Team> Teamlist { get; set; }
        public List<Match> Matchlist { get; set; }

        //user input
        public int SelectedTourId { get; set; }
        public int SelectedHomeTeamId { get; set;  }
        public int SelectedAwayTeamId { get; set; }
        public DateTime InputDate { get; set; }
    }
}