using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Predictions.Models;

namespace Predictions.DAL.FetchStrategies.TournamentsFetchStrategies
{
    public class FetchToursWithMatchesWithHomeTeam : IFetchStrategy<Tournament>
    {
        public Expression<Func<Models.Tournament, object>> Apply()
        {
            return t => t.NewTours.
                Select(tr => tr.Matches
                    .Select(m => m.HomeTeam));
        }
    }
}