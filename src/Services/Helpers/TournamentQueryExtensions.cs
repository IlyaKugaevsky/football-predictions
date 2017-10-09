using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;

namespace Services.Helpers
{
    public static class TournamentQueryExtensions
    {
        public static Tournament TournamentById(this IEnumerable<Tournament> tours, int tournamentId)
        {
            return tours.Single(t => t.TournamentId == tournamentId);
        }

    }
}
