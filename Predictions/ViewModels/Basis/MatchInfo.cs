using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Predictions.Models;

namespace Predictions.ViewModels
{
    public class MatchInfo
    {
        public DateTime Date { get; set; }
        public string HomeTeamTitle { get; set; }
        public string AwayTeamTitle { get; set; }
        public string PredictionValue { get; set; }
        public string Score { get; set; }
    }

    public class TourInfo
    {
        public List<MatchInfo> Matchlist { get; set; }
        public int TourId { get; set; }
    }
}