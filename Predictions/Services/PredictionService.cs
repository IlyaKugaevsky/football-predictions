using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Predictions.DAL;
using System.Data.Entity;

namespace Predictions.Services
{
    public class PredictionService
    {

        public void SubmitTourPredictions(int tourId, PredictionsContext context)
        {
            var tour = context.Tours
                   .Include(t => t.Matches
                       .Select(m => m.HomeTeam))
                   .Include(t => t.Matches
                       .Select(m => m.AwayTeam))
                   .Include(t => t.Matches
                       .Select(m => m.Predictions))
                   .SingleOrDefault(t => t.TourId == tourId);
        }
    }
}