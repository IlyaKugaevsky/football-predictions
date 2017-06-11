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

        public MatchTableViewModel (List<string> headers, List<MatchInfo> matchlist = null, IList<FootballScore> scorelist = null, List<ActionLinkParams> actionLinklist = null)
        {
            Headers = headers;
            Matchlist = matchlist ?? new List<MatchInfo>();
            Scorelist = scorelist ?? new List<FootballScore>();
            ActionLinklist = actionLinklist ?? new List<ActionLinkParams>();
        }

        public MatchTableViewModel(List<string> headers, bool isEditable, string emptyScoreSymbol, List<MatchInfo> matchlist = null, IList<FootballScore> scorelist = null, List<ActionLinkParams> actionLinklist = null)
        {
            Headers = headers;
            Matchlist = matchlist ?? new List<MatchInfo>();
            Scorelist = scorelist ?? new List<FootballScore>();
            ActionLinklist = actionLinklist ?? new List<ActionLinkParams>();
            IsEditable = isEditable;
            EmptyScoreSymbol = emptyScoreSymbol;
        }

        public List<string> Headers { get; set; }
        public List<MatchInfo> Matchlist { get; set; }
        public IList<FootballScore> Scorelist { get; set; }
        public IList<ActionLinkParams> ActionLinklist { get; set; }

        public bool IsEditable { get; set; }
        public string EmptyScoreSymbol { get; set; }

    }
}