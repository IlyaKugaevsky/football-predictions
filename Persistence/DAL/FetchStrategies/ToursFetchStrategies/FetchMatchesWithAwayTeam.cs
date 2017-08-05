using System;
using System.Linq;
using System.Linq.Expressions;
using Core.Core.Models;

namespace Persistence.DAL.FetchStrategies.ToursFetchStrategies
{
    public class FetchMatchesWithAwayTeam : IFetchStrategy<Tour>
    {
        public Expression<Func<Tour, object>> Apply()
        {
            return t => t.Matches.Select(m => m.AwayTeam);
        }
    }
}