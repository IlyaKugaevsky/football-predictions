using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Predictions.DAL;
using Predictions.DAL.EntityFrameworkExtensions;
using Predictions.DAL.FetchStrategies;
using Predictions.DAL.FetchStrategies.ExpertsFetchStrategies;
using Predictions.DAL.FetchStrategies.ToursFetchStrategies;
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
                new FetchMatchesWithHomeTeam(), 
                new FetchMatchesWithAwayTeam(),
                new FetchMatchesWithPredictions() 
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

        public List<ExpertStat> GenerateExpertsOverallRating()
        {
            var fetchStrategies = new IFetchStrategy<Expert>[] { new FetchPredictions() };
            var experts = _context.GetExperts(fetchStrategies).ToList();
            var validExperts = experts.Where(e => !e.Predictions.ToList().IsNullOrEmpty());

            return validExperts.Select(e => new ExpertStat(e.Nickname,
                                                           e.Predictions.Count,
                                                           Math.Round((double)e.GetPredictionsSum() / e.Predictions.Count, 2),
                                                           e.GetPredictionsSum()))
                                                           .ToList();
        }

    }
}