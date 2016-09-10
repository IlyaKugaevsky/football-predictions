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

        public MatchTableViewModel (List<string> headers, List<MatchInfo> matchlist = null, List<FootballScore> scorelist = null, List<ActionLinkParams> actionLinklist = null)
        {
            Headers = headers;
            Matchlist = matchlist ?? new List<MatchInfo>();
            Scorelist = scorelist ?? new List<FootballScore>();
            ActionLinklist = actionLinklist ?? new List<ActionLinkParams>();
        }

        public List<string> Headers { get; set; }
        public List<MatchInfo> Matchlist { get; set; }
        public List<FootballScore> Scorelist { get; set; }
        public List<ActionLinkParams> ActionLinklist { get; set; } 

    }
}