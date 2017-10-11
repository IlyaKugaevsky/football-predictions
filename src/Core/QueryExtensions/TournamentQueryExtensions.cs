using System.Collections.Generic;
using System.Linq;
using Core.Models;

namespace Core.QueryExtensions
{
    public static class TournamentQueryExtensions
    {
        public static Tournament TournamentById(this IEnumerable<Tournament> tournaments, int tournamentId)
        {
            return tournaments.Single(t => t.TournamentId == tournamentId);
        }

        public static Tournament LastAdded(this IEnumerable<Tournament> tournaments)
        {
            return tournaments.OrderByDescending(t => t.TournamentId).First(); ;
        }
    }
}
