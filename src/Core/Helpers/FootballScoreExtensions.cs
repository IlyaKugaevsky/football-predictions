using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using Core.Models.Dtos;

namespace Core.Helpers
{
    public static class FootballScoreExtensions
    {
        public static IEnumerable<FootballScore> GetScores(this IEnumerable<Match> matches) => 
            matches.Select(m => m.GetFootballScore());

        public static IEnumerable<FootballScoreViewModel> ToViewModels(this IEnumerable<FootballScore> scores, bool editable) =>
            scores.Select(s => s.GenerateViewModel(editable)); 
    }
}
