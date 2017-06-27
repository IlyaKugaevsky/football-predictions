using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Predictions.ViewModels.Basis
{
    public class MatchStat
    {
        public MatchStat(string homeTeamTitle, string awayTeamTitle, double averageSum, int differentPredictions)
        {
            HomeTeamTitle = homeTeamTitle;
            AwayTeamTitle = awayTeamTitle;
            AverageSum = averageSum;
            DifferentPredictions = differentPredictions;
        }
        public string HomeTeamTitle { get; set; }
        public string AwayTeamTitle { get; set; }
        public double AverageSum { get; set; }
        public int DifferentPredictions { get; set; }
    }
}