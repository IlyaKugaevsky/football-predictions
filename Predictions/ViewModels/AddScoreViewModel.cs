using Predictions.Models;
using Predictions.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Predictions.ViewModels
{
    public class AddScoresViewModel
    {
        public int CurrentTourId { get; set; }

        //display
        public List<MatchInfo> Matchlist { get; set; }

        //input
        public List<string> InputScorelist { get; set; }
    }
}