using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Models;
using Core.Models.Dtos;
using Web.ViewModels.Basis;
using FootballScore = Core.Models.Dtos.FootballScore;

namespace Web.ViewModels
{
    public class EditTourViewModel
    {
        public EditTourViewModel()
        { }

        public EditTourViewModel(List<Team> teams, IReadOnlyCollection<Match> matches, List<FootballScore> scorelist,  TourDto tourDto)
        {
            Teamlist = GenerateSelectList(teams);
            TourDto = tourDto;
            MatchTable = GenerateMatchTable(matches, scorelist);
            SubmitTextArea = new SubmitTextAreaViewModel(tourDto.TourId);
        }

        public TourDto TourDto { get; set; }
        public MatchTableViewModel MatchTable { get; set; }
        public List<SelectListItem> Teamlist { get; set; }
        public int SelectedHomeTeamId { get; set; }
        public int SelectedAwayTeamId { get; set; }
        public DateTime InputDate { get; set; }
        public SubmitTextAreaViewModel SubmitTextArea { get; set; }

        private List<SelectListItem> GenerateSelectList(List<Team> teams)
        {
            return teams.Select(t => new SelectListItem()
            {
                Text = t.Title,
                Value = t.TeamId.ToString()
            }).ToList();
        }

        private MatchTableViewModel GenerateMatchTable(IReadOnlyCollection<Match> matches, List<FootballScore> scorelist)
        {
            var headers = new List<string>() { "Дата", "Дома", "В гостях", "Счет" };

            var matchlist = matches.Select(m => m.GetDto()).ToList();

            var actionLinklist = matches.Select(m =>
                new ActionLinkParams(
                    "Удалить",
                    "DeleteConfirmation",
                    null,
                    new { id = m.MatchId },
                    new { @class = "btn btn-default" })).ToList();

            return new MatchTableViewModel(headers, matchlist, scorelist, actionLinklist);

        }
    }
}