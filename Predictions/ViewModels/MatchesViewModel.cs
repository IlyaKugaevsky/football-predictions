using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Models;

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