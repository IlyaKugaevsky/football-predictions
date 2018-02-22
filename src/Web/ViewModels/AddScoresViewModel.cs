using Web.ViewModels.Basis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Models.Dtos;

namespace Web.ViewModels
{
    public class AddScoresViewModel
    {
        public AddScoresViewModel()
        { }

        public AddScoresViewModel(int currentTourNumber, List<MatchDto> matches, IList<FootballScoreViewModel> scorelist)
        {
            CurrentTourNumber = currentTourNumber;
            MatchTable = GenerateMatchTable(matches, scorelist);
        }

        public int CurrentTourNumber { get; set; }

        public MatchTableViewModel MatchTable {get; set;}

        private MatchTableViewModel GenerateMatchTable(List<MatchDto> matches, IList<FootballScoreViewModel> scorelist)
        {
            var headers = new List<string>() { "Дата", "Дома", "В гостях", "Счет" };
            return new MatchTableViewModel(headers, matches, scorelist);
        }
    }
}