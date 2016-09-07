using Predictions.ViewModels.Basis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Predictions.ViewModels
{
    public class MatchTableViewModel
    {
        public MatchTableViewModel()
        { }

        public MatchTableViewModel (List<string> headers, List<MatchInfo> matchlist, List<FootballScore> scorelist)
        {
            Headers = headers;
            Matchlist = matchlist;
            Scorelist = scorelist;
        }

        public List<string> Headers { get; set; }
        public List<MatchInfo> Matchlist { get; set; }
        public List<FootballScore> Scorelist { get; set; }
    }
}