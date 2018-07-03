using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.DAL.FetchStrategies.ToursFetchStrategies
{
    public class FetchMatchesWithPredictionsWithExperts: IFetchStrategy<Tour>
    {
        public Expression<Func<Tour, object>> Apply()
        {
            return t => t.Matches
                .Select(m => m.Predictions
                    .Select(p => p.Expert));
        }

    }
}
