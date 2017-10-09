using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;

namespace Services.Helpers
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
    }
}
