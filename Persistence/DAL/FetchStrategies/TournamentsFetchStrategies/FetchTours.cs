using System;
using System.Linq.Expressions;
using Core.Core.Models;

namespace Persistence.DAL.FetchStrategies.TournamentsFetchStrategies
{
    public class FetchTours : IFetchStrategy<Tournament>
    {
        public Expression<Func<Tournament, object>> Apply()
        {
            return t => t.NewTours;
        }
    }
}