using System.Linq;
using Core.Models;
using Persistence.DAL.FetchStrategies;

namespace Persistence.DAL.EntityFrameworkExtensions
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