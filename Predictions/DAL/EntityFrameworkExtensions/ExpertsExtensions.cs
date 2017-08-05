using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Predictions.Core.Models;
using Predictions.DAL.FetchStrategies;

namespace Predictions.DAL.EntityFrameworkExtensions
{
    public static class ExpertsExtensions
    {
        public static IQueryable<Expert> GetExperts(this PredictionsContext context, IFetchStrategy<Expert>[] fetchStrategies)
        {
            var appliedStrategies = fetchStrategies.Select(fs => fs.Apply()).ToArray();
            return context.Experts.IncludeMultiple<Expert>(appliedStrategies);
        }
    }
}