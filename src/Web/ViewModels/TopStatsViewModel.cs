using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Models.Dtos;
using Web.ViewModels.Basis;

namespace Web.ViewModels
{
    public class TopStatsViewModel
    {
        public TopStatsViewModel(TopStats topStats)
        {
            var headers = new List<string>() {"Дата", "Дома", "В гостях"};
            MostPredicatble = new MatchTableViewModel(headers, topStats.MostPredicatble);
            LeastPredicatble = new MatchTableViewModel(headers, topStats.LeastPredicatble);
            MostPredictions = new MatchTableViewModel(headers, topStats.MostPredictions);
            LeastPredictions = new MatchTableViewModel(headers, topStats.LeastPredictions);

        }
        public MatchTableViewModel MostPredicatble { get; private set; }
        public MatchTableViewModel LeastPredicatble { get; private set; }
        public MatchTableViewModel MostPredictions { get; private set; }
        public MatchTableViewModel LeastPredictions { get; private set; }

    }
}