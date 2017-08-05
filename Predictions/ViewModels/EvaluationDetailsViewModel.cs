using Predictions.ViewModels.Basis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Predictions.Core.Models;
using Predictions.Core.Models.Dtos;
using Predictions.DAL;
using Predictions.DAL.EntityFrameworkExtensions;
using FootballScore = Predictions.Core.Models.Dtos.FootballScore;

namespace Predictions.ViewModels
{
    public class EvaluationDetailsViewModel
    {
        public EvaluationDetailsViewModel()
        {
        }

        public EvaluationDetailsViewModel(List<MatchDto> matchlist, IList<FootballScore> scorelist, List<FootballScore> predictionlist, List<string> tempResultlist)
        {
            Matchlist = matchlist ?? new List<MatchDto>();
            Scorelist = scorelist ?? new List<FootballScore>();
            Predictionlist = predictionlist ?? new List<FootballScore>();
            TempResultlist = tempResultlist ?? new List<string>();
            if (!tempResultlist.IsNullOrEmpty() && !tempResultlist.Contains("-")) Sum = tempResultlist.Select(tr => Convert.ToInt32(tr)).Sum();
        }
        public List<MatchDto> Matchlist { get; set; }
        public IList<FootballScore> Scorelist { get; set; }
        public List<FootballScore> Predictionlist { get; set; }
        public List<string> TempResultlist { get; set; }

        public int Sum = -1;
    }
}