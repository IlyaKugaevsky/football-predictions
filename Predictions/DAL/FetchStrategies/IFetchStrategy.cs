using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Security.Cookies;
using Predictions.Models;

namespace Predictions.DAL.FetchStrategies
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
