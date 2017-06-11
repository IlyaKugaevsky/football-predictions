using Predictions.ViewModels.Basis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Predictions.DAL;

namespace Predictions.ViewModels
{
    public class EvaluationDetailsViewModel
    {
        public EvaluationDetailsViewModel()
        {
        }

        public EvaluationDetailsViewModel(List<MatchInfo> matchlist, IList<FootballScore> scorelist, List<FootballScore> predictionlist, List<string> tempResultlist)
        {
            Matchlist = matchlist ?? new List<MatchInfo>();
            Scorelist = scorelist ?? new List<FootballScore>();
            Predictionlist = predictionlist ?? new List<FootballScore>();
            TempResultlist = tempResultlist ?? new List<string>();
            if (!tempResultlist.IsNullOrEmpty() && !tempResultlist.Contains("-")) Sum = tempResultlist.Select(tr => Convert.ToInt32(tr)).Sum();
        }
        public List<MatchInfo> Matchlist { get; set; }
        public IList<FootballScore> Scorelist { get; set; }
        public List<FootballScore> Predictionlist { get; set; }
        public List<string> TempResultlist { get; set; }

        public int Sum = -1;
    }
}