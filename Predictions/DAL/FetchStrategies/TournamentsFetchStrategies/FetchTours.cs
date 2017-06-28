using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Predictions.Models;

namespace Predictions.DAL.FetchStrategies.TournamentsFetchStrategies
{
    public class FetchTours : IFetchStrategy<Tournament>
    {
        public Expression<Func<Tournament, object>> Apply()
        {
            return t => t.NewTours;
        }
    }
}