using System;
using System.Linq.Expressions;
using Core.Core.Models;

namespace Persistence.DAL.FetchStrategies.MatchesFetchStrategies
{
    public class FetchHomeTeam: IFetchStrategy<Match>
    {
        public Expression<Func<Match, object>> Apply()
        {
            return m => m.HomeTeam;
        }
    }
}