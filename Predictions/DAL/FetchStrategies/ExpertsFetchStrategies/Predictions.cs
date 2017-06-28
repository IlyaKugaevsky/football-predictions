using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Predictions.Models;

namespace Predictions.DAL.FetchStrategies.ExpertsFetchStrategies
{
    public class FetchPredictions: IFetchStrategy<Expert>
    {
        public Expression<Func<Expert, object>> Apply()
        {
            return e => e.Predictions;
        }
    }
}