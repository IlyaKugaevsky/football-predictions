using Web.ViewModels.Basis;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web.Mvc;
using Core.Models;
using Core.Models.Dtos;
using FootballScore = Core.Models.Dtos.FootballScore;

namespace Web.ViewModels
{
    public class EditPredictionsViewModel
    {
        public EditPredictionsViewModel()
        { }

        //public EditPredictionsViewModel(List<SelectListItem> expertlist, NewTourDto newTourDto, MatchTableViewModel matchTable)
        //{
        //    NewTourDto = newTourDto;
        //    Expertlist = expertlist;
        //    MatchTable = matchTable;
        //    SubmitTextArea = new SubmitTextAreaViewModel();
        //}

        public EditPredictionsViewModel(IReadOnlyCollection<Match> matches, IReadOnlyList<Expert> experts, List<FootballScore> scorelist, TourDto tourDto, int expertId, bool addPredictionSuccess)
        {
            TourDto = tourDto;
            Expertlist = GenerateSelectList(experts);
            MatchTable = GenerateMatchTable(matches, scorelist);
            SubmitTextArea = new SubmitTextAreaViewModel();
            SelectedExpertId = expertId;
            SubmitTextArea.TourId = tourDto.TourId;
            AddPredictionsSuccess = addPredictionSuccess;
        }

        //display
        public TourDto TourDto { get; set; }

        public IReadOnlyList<SelectListItem> Expertlist { get; set; }

        public MatchTableViewModel MatchTable { get; set; }

        //input
        public int SelectedExpertId { get; set; }

        public SubmitTextAreaViewModel SubmitTextArea { get; set; }

        public bool AddPredictionsSuccess { get; set; } = false;

        private IReadOnlyList<SelectListItem> GenerateSelectList(IEnumerable<Expert> experts)
        {
            return experts.Select(e => new SelectListItem()
            {
                Text = e.Nickname,
                Value = e.ExpertId.ToString()
            }).ToList();
        }

        private MatchTableViewModel GenerateMatchTable(IEnumerable<Match> matches, List<FootballScore> scorelist)
        {
            var headers = new List<string>() { "Дата", "Дома", "В гостях", "Прогноз" };
            var matchlist = matches.Select(m => m.GetDto()).ToList();

            return new MatchTableViewModel(headers, matchlist, scorelist);
        }
    }
}