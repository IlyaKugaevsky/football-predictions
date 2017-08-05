using System.Collections.Generic;
using System.Linq;
using Core.Core.Models;
using Persistence.DAL.FetchStrategies;

namespace Persistence.DAL.EntityFrameworkExtensions
{
    public static class ExpertsExtensions
    {
        public static IQueryable<Expert> GetExperts(this PredictionsContext context, IFetchStrategy<Expert>[] fetchStrategies = null)
        {
            if (fetchStrategies == null) return context.Experts;
            var appliedStrategies = fetchStrategies.Select(fs => fs.Apply()).ToArray();
            return context.Experts.IncludeMultiple<Expert>(appliedStrategies);
        }
    }
}