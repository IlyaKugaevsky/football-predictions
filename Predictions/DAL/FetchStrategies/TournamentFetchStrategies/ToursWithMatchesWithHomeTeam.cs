using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Predictions.Models;

namespace Predictions.DAL.FetchStrategies.TournamentFetchStrategies
{
    public class ToursWithMatchesWithHomeTeam : IFetchStrategy<Tournament>
    {
        public Expression<Func<Models.Tournament, object>> Apply()
        {
            return t => t.Tours.
                Select(tr => tr.Matches
                    .Select(m => m.HomeTeam));
        }
    }
}