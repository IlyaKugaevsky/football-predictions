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

        public AddScoresViewModel(int currentTourId, MatchTableViewModel matchTable)
        {
            CurrentTourId = currentTourId;
            MatchTable = matchTable;
        }

        public int CurrentTourId { get; set; }

        ////display
        //public List<MatchInfo> Matchlist { get; set; }

        ////input and display
        //public List<FootballScore> EditScorelist { get; set; }

        public MatchTableViewModel MatchTable {get; set;}
    }
}