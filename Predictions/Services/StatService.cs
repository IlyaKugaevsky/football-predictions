using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Predictions.DAL;
using Predictions.DAL.EntityFrameworkExtensions;
using Predictions.DAL.FetchStrategies;
using Predictions.DAL.FetchStrategies.TourFetchStrategies;
using Predictions.Models;
using Predictions.ViewModels.Basis;

namespace Predictions.Services
{
    public class StatService
    {
        private readonly PredictionsContext _context;

        public StatService(PredictionsContext context)
        {
            _context = context;
        }

        public List<MatchStat> GenerateMatchStats(int tourId)
        {
            var fetchStrategies = new IFetchStrategy<Tour>[]
            {
                new MatchesWithHomeTeam(), 
                new MatchesWithAwayTeam(),
                new MatchesWithPredictions() 
            };

            var tour = _context.GetTours(fetchStrategies).Single(t => t.TourId == tourId);
            if(!tour.IsClosed) return null;

            var matches = tour.Matches;
            var matchStats = new List<MatchStat>();

            foreach (var match in matches)
            {
                double predictionAverageSum = 0;
                var predictionValues = new HashSet<string>();

                foreach (var prediction in match.Predictions)
                {
                    predictionAverageSum += prediction.Sum;
                    predictionValues.Add(prediction.Value);
                }

                predictionAverageSum /= match.Predictions.Count;
                var differentPredictions = predictionValues.Count;
                matchStats.Add(new MatchStat(
                    match.HomeTeam.Title, 
                    match.AwayTeam.Title, 
                    Math.Round(predictionAverageSum, 2), 
                    differentPredictions));
            }

            return matchStats;
        }

    }
}