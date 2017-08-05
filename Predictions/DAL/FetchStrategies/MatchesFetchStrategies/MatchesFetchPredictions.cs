using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Predictions.Core.Models;

namespace Predictions.DAL.FetchStrategies.MatchesFetchStrategies
{
    public class MatchesFetchPredictions: IFetchStrategy<Match>
    {
        public Expression<Func<Match, object>> Apply()
        {
            return m => m.Predictions;
        }
    }
}