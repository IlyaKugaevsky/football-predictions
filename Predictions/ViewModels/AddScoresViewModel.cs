using Predictions.Models;
using Predictions.ViewModels.Basis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Predictions.ViewModels
{
    public class AddScoresViewModel
    {
        public AddScoresViewModel()
        { }

        public AddScoresViewModel(int currentTourId, List<MatchInfo> matchlist)
        {
            CurrentTourId = currentTourId;
            Matchlist = matchlist;
        }

        public int CurrentTourId { get; set; }

        //display
        public List<MatchInfo> Matchlist { get; set; }

        //input
        public List<FootballScore> InputScorelist { get; set; }
    }
}