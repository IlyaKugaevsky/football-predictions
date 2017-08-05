using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;
using Predictions.Core.Models;

namespace Predictions.DAL.FetchStrategies.TournamentsFetchStrategies
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