using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using Persistence.DAL;
using Persistence.DAL.EntityFrameworkExtensions;
using Persistence.DAL.FetchStrategies;

namespace Services.Helpers
{
    public static class TourQueryExtensions
    {
        public static Tour TourById(this IEnumerable<Tour> tours, int tourId)
        {
            return tours.Single(t => t.TourId == tourId);
        }
    }
}
