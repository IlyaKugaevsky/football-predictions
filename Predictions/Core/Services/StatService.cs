using System;
using System.Collections.Generic;
using System.Linq;
using Predictions.Core.Models;
using Predictions.Core.Models.Dtos;
using Predictions.DAL;
using Predictions.DAL.EntityFrameworkExtensions;
using Predictions.DAL.FetchStrategies;
using Predictions.DAL.FetchStrategies.MatchesFetchStrategies;
using Predictions.DAL.FetchStrategies.ToursFetchStrategies;

namespace Predictions.Core.Services
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
            var fetchStrategies = new IFetchStrategy<Expert>[] { new DAL.FetchStrategies.ExpertsFetchStrategies.ExpertsFetchPredictions() };
            var experts = _context.GetExperts(fetchStrategies).ToList();
            var validExperts = experts.Where(e => !e.Predictions.ToList().IsNullOrEmpty());

            return validExperts.Select(e => new ExpertStat(e.Nickname,
                                                           e.Predictions.Count,
                                                           Math.Round((double)e.GetPredictionsSum() / e.Predictions.Count, 2),
                                                           e.GetPredictionsSum()))
                                                           .ToList();
        }

        public TopStats GenerateTopStats()
        {
            var fetchStrategies = new IFetchStrategy<Match>[]
            {
                new FetchHomeTeam(),
                new FetchAwayTeam(), 
                new MatchesFetchPredictions()
            };

            var matches = _context.GetMatches(fetchStrategies).ToList();

            var leastPredictable = new List<Match> { matches.First() };
            var mostPredictable = new List<Match> { matches.First() };
            var littlePredictions = new List<Match> { matches.First() };
            var manyPredictions = new List<Match> { matches.First() };

            foreach (var match in matches.Where(m => !m.Predictions.IsNullOrEmpty()))
            {
                var sum = Enumerable.Sum((IEnumerable<int>) match.Predictions.Select(p => p.Sum));
                var avgSum = Math.Round((double) sum / match.Predictions.Count, 2);

                var differentPredictions = match.Predictions
                                                .GroupBy(p => p.Value)
                                                .First()
                                                .Count();

                if (sum > mostPredictable.First().GetPredictionsSum())
                {
                    mostPredictable.Clear();
                    mostPredictable.Add(match);
                }
                else if (sum == mostPredictable.First().GetPredictionsSum()) mostPredictable.Add(match);

                if (sum < leastPredictable.First().GetPredictionsSum())
                {
                    leastPredictable.Clear();
                    leastPredictable.Add(match);
                }
                else if (sum == leastPredictable.First().GetPredictionsSum()) leastPredictable.Add(match);

                if (match.Predictions.Count > manyPredictions.First().Predictions.Count)
                {
                    manyPredictions.Clear();
                    manyPredictions.Add(match);
                }
                else if (match.Predictions.Count == manyPredictions.First().Predictions.Count) manyPredictions.Add(match);

                if (match.Predictions.Count < littlePredictions.First().Predictions.Count)
                {
                    littlePredictions.Clear();
                    littlePredictions.Add(match);
                }
                else if (match.Predictions.Count == littlePredictions.First().Predictions.Count) littlePredictions.Add(match);
            }

            return new TopStats(mostPredictable.Select(m => m.GetDto()).ToList(), 
                                leastPredictable.Select(m => m.GetDto()).ToList(), 
                                manyPredictions.Select(m => m.GetDto()).ToList(), 
                                littlePredictions.Select(m => m.GetDto()).ToList());

            //var predictions = matches.SelectMany(m => m.Predictions);

            //var groupedPredictions = predictions.GroupBy(p => p.Value)
            //                                    .OrderByDescending(g => g.Count());
        }

    }
}