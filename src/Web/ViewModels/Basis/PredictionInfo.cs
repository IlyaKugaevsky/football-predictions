using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels.Basis
{
    public class PredictionInfo
    {
        public PredictionInfo()
        { }

        public PredictionInfo(string homeTitle, string awayTitle, string predictionValue)
        {
            HomeTeamTitle = homeTitle;
            AwayTeamTitle = awayTitle;
            PredictionValue = predictionValue; 
        }

        public string HomeTeamTitle { get; set; }
        public string AwayTeamTitle { get; set; }
        public string PredictionValue { get; set; }
    }
}