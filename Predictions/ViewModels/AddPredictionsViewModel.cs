using Predictions.Models;
using Predictions.ViewModels.Basis;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web.Mvc;
using Predictions.Models.Dtos;

namespace Predictions.ViewModels
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

        public EditPredictionsViewModel(List<Match> matches, List<Expert> experts, List<FootballScore> scorelist, NewTourDto newTourDto, int expertId, bool addPredictionSuccess)
        {
            NewTourDto = newTourDto;
            Expertlist = GenerateSelectList(experts);
            MatchTable = GenerateMatchTable(matches, scorelist);
            SubmitTextArea = new SubmitTextAreaViewModel();
            SelectedExpertId = expertId;
            SubmitTextArea.TourId = newTourDto.TourId;
            AddPredictionsSuccess = addPredictionSuccess;
        }

        //display
        public NewTourDto NewTourDto { get; set; }

        public List<SelectListItem> Expertlist { get; set; }

        public MatchTableViewModel MatchTable { get; set; }

        //input
        public int SelectedExpertId { get; set; }

        public SubmitTextAreaViewModel SubmitTextArea { get; set; }

        public bool AddPredictionsSuccess { get; set; } = false;

        private List<SelectListItem> GenerateSelectList(List<Expert> experts)
        {
            return experts.Select(e => new SelectListItem()
            {
                Text = e.Nickname,
                Value = e.ExpertId.ToString()
            }).ToList();
        }

        private MatchTableViewModel GenerateMatchTable(List<Match> matches, List<FootballScore> scorelist)
        {
            var headers = new List<string>() { "Дата", "Дома", "В гостях", "Прогноз" };
            var matchlist = matches.Select(m => m.GetDto()).ToList();

            return new MatchTableViewModel(headers, matchlist, scorelist);
        }
    }
}