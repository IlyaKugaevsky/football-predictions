using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Predictions.Core.Models;
using Predictions.DAL.FetchStrategies;
using Predictions.DAL.FetchStrategies.TournamentsFetchStrategies;

namespace Predictions.DAL.EntityFrameworkExtensions
{
    public static class MatchesExtensions
    {
        public static IQueryable<Match> GetMatches(this PredictionsContext context, IFetchStrategy<Match>[] fetchStrategies)
        {
            var appliedStrategies = fetchStrategies.Select(fs => fs.Apply()).ToArray();
            return context.Matches.IncludeMultiple<Match>(appliedStrategies);
        }
    }
}