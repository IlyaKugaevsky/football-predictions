using System;
using System.Linq.Expressions;
using Core.Models;
using System.Linq;

namespace Persistence.DAL.FetchStrategies.TournamentsFetchStrategies
{
    public class FetchToursWithMatchesWithHomeTeam : IFetchStrategy<Tournament>
    {
        public Expression<Func<Tournament, object>> Apply()
        {
            return t => t.NewTours.
                Select(tr => tr.Matches
                    .Select(m => m.HomeTeam));
        }
    }
}