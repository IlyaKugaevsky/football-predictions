using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Models.Dtos;
using Web.ViewModels.Basis;

namespace Web.ViewModels
{
    public class MatchTableViewModel
    {
        public MatchTableViewModel()
        { }

        public MatchTableViewModel (List<string> headers, List<MatchDto> matchlist = null, IList<FootballScoreViewModel> scorelist = null, List<ActionLinkParams> actionLinklist = null)
        {
            Headers = headers;
            Matchlist = matchlist ?? new List<MatchDto>();
            Scorelist = scorelist ?? new List<FootballScoreViewModel>();
            ActionLinklist = actionLinklist ?? new List<ActionLinkParams>();
        }

        public MatchTableViewModel(List<string> headers, bool isEditable, string emptyScoreSymbol, List<MatchDto> matchlist = null, IList<FootballScoreViewModel> scorelist = null, List<ActionLinkParams> actionLinklist = null)
        {
            Headers = headers;
            Matchlist = matchlist ?? new List<MatchDto>();
            Scorelist = scorelist ?? new List<FootballScoreViewModel>();
            ActionLinklist = actionLinklist ?? new List<ActionLinkParams>();
            IsEditable = isEditable;
            EmptyScoreSymbol = emptyScoreSymbol;
        }

        public List<string> Headers { get; set; }
        public List<MatchDto> Matchlist { get; set; }
        public IList<FootballScoreViewModel> Scorelist { get; set; }
        public IList<ActionLinkParams> ActionLinklist { get; set; }

        public bool IsEditable { get; set; }
        public string EmptyScoreSymbol { get; set; }

    }
}