using System;
using System.Linq;
using System.Linq.Expressions;
using Core.Models;

namespace Persistence.DAL.FetchStrategies.TournamentsFetchStrategies
{
    public class FetchToursWithMatchesWithAwayTeam : IFetchStrategy<Tournament>
    {
        public Expression<Func<Tournament, object>> Apply()
        {
            return t => t.NewTours.
                Select(tr => tr.Matches
                    .Select(m => m.AwayTeam));
        }
    }
}