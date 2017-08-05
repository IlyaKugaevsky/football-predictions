using System;
using System.Linq.Expressions;
using Core.Models;

namespace Persistence.DAL.FetchStrategies.ToursFetchStrategies
{
    public class FetchMatches : IFetchStrategy<Tour>
    {
        public Expression<Func<Tour, object>> Apply()
        {
            return t => t.Matches;
        }
    }
}