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

        public AddScoresViewModel(int currentTourId, List<MatchInfo> matchlist, List<FootballScore> scorelist)
        {
            CurrentTourId = currentTourId;
            Matchlist = matchlist;
            EditScorelist = scorelist;
        }

        public int CurrentTourId { get; set; }

        //display
        public List<MatchInfo> Matchlist { get; set; }

        //input and display
        public List<FootballScore> EditScorelist { get; set; }
    }
}