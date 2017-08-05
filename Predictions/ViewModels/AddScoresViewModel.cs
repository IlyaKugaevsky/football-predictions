using Predictions.ViewModels.Basis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Predictions.Core.Models;
using Predictions.Core.Models.Dtos;
using FootballScore = Predictions.Core.Models.Dtos.FootballScore;

namespace Predictions.ViewModels
{
    public class AddScoresViewModel
    {
        public AddScoresViewModel()
        { }

        public AddScoresViewModel(int currentTourId, List<MatchDto> matches, List<FootballScore> scorelist)
        {
            CurrentTourId = currentTourId;
            MatchTable = GenerateMatchTable(matches, scorelist);
        }

        public int CurrentTourId { get; set; }

        public MatchTableViewModel MatchTable {get; set;}

        private MatchTableViewModel GenerateMatchTable(List<MatchDto> matches, List<FootballScore> scorelist)
        {
            var headers = new List<string>() { "Дата", "Дома", "В гостях", "Счет" };
            return new MatchTableViewModel(headers, matches, scorelist);
        }
    }
}