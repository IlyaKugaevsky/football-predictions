using System;
using System.Linq.Expressions;
using Core.Models;

namespace Persistence.DAL.FetchStrategies.ExpertsFetchStrategies
{
    public class ExpertsFetchPredictions: IFetchStrategy<Expert>
    {
        public Expression<Func<Expert, object>> Apply()
        {
            return e => e.Predictions;
        }
    }
}