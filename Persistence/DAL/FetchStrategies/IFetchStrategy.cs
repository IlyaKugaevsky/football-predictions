using System;
using System.Linq.Expressions;

namespace Persistence.DAL.FetchStrategies
{
    public interface IFetchStrategy<T>
    {
        Expression<Func<T, object>> Apply();
    }

    //public class HomeTeamFetchStrategy : IFetchStrategy<Tour>
    //{
    //    public Expression<Func<Tour, object>> Apply()
    //    {
    //        return x => x.Matches.Select(m => m.HomeTeam);
    //    }
    //}
}
