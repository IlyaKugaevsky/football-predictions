using System.Collections.Generic;
using System.Linq;
using Core.Models;
using Core.Models.Dtos;
using Core.Helpers;

namespace Core.QueryExtensions
{
    public static class MatchQueryExtensions
    {
        public static Match MatchById(this IEnumerable<Match> matches, int matchId)
        {
            return matches.Single(m => m.MatchId == matchId);
        }

        public static bool AllScoresNotNullOrEmpty(this IEnumerable<Match> matches)
        {
            return !matches.Any(m => m.Score.IsNullOrEmpty());
        }

        public static bool AllHaveBothTeams(this IEnumerable<Match> matches)
        {
            return !matches.Any(m => m.HomeTeam == null || m.AwayTeam == null);
        }

        //public static IEnumerable<MatchDto> ToDtos(this IEnumerable<Match> matches)
        //{
        //    return matches.Select(m => m.GetDto());
        //}
    }
}
