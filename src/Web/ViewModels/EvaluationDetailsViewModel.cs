using Web.ViewModels.Basis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Helpers;
using Core.Models.Dtos;

namespace Web.ViewModels
{
    public class EvaluationDetailsViewModel
    {
        public EvaluationDetailsViewModel()
        {
        }

        public EvaluationDetailsViewModel(List<MatchDto> matchlist, IList<FootballScoreViewModel> scorelist, List<FootballScoreViewModel> predictionlist, List<string> tempResultlist)
        {
            Matchlist = matchlist ?? new List<MatchDto>();
            Scorelist = scorelist ?? new List<FootballScoreViewModel>();
            Predictionlist = predictionlist ?? new List<FootballScoreViewModel>();
            TempResultlist = tempResultlist ?? new List<string>();
            if (!GenericsHelper.IsNullOrEmpty(tempResultlist) && !tempResultlist.Contains("-")) Sum = tempResultlist.Select(tr => Convert.ToInt32(tr)).Sum();
        }
        public List<MatchDto> Matchlist { get; set; }
        public IList<FootballScoreViewModel> Scorelist { get; set; }
        public List<FootballScoreViewModel> Predictionlist { get; set; }
        public List<string> TempResultlist { get; set; }

        public int Sum = -1;
    }
}