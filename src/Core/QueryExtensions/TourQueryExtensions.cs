using System.Collections.Generic;
using System.Linq;
using Core.Models;

namespace Core.QueryExtensions
{
    public static class TourQueryExtensions
    {
        public static Tour TourById(this IEnumerable<Tour> tours, int tourId)
        {
            return tours.Single(t => t.TourId == tourId);
        }
    }
}
