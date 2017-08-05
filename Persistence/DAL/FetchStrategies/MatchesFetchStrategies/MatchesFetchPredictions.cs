using System;
using System.Linq.Expressions;
using Core.Models;

namespace Persistence.DAL.FetchStrategies.MatchesFetchStrategies
{
    public class MatchesFetchPredictions: IFetchStrategy<Match>
    {
        public Expression<Func<Match, object>> Apply()
        {
            return m => m.Predictions;
        }
    }
}