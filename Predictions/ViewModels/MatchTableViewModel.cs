using Predictions.ViewModels.Basis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Models.Dtos;
using FootballScore = Core.Models.Dtos.FootballScore;

namespace Predictions.ViewModels
{
    public class MatchTableViewModel
    {
        public MatchTableViewModel()
        { }

        public MatchTableViewModel (List<string> headers, List<MatchDto> matchlist = null, List<FootballScore> scorelist = null, List<ActionLinkParams> actionLinklist = null)
        {
            Headers = headers;
            Matchlist = matchlist ?? new List<MatchDto>();
            Scorelist = scorelist ?? new List<FootballScore>();
            ActionLinklist = actionLinklist ?? new List<ActionLinkParams>();
        }

        public MatchTableViewModel(List<string> headers, bool isEditable, string emptyScoreSymbol, List<MatchDto> matchlist = null, IList<FootballScore> scorelist = null, List<ActionLinkParams> actionLinklist = null)
        {
            Headers = headers;
            Matchlist = matchlist ?? new List<MatchDto>();
            Scorelist = scorelist ?? new List<FootballScore>();
            ActionLinklist = actionLinklist ?? new List<ActionLinkParams>();
            IsEditable = isEditable;
            EmptyScoreSymbol = emptyScoreSymbol;
        }

        public List<string> Headers { get; set; }
        public List<MatchDto> Matchlist { get; set; }
        public IList<FootballScore> Scorelist { get; set; }
        public IList<ActionLinkParams> ActionLinklist { get; set; }

        public bool IsEditable { get; set; }
        public string EmptyScoreSymbol { get; set; }

    }
}