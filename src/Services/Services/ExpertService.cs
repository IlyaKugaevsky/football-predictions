using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Core;
using Core.Models;
using Persistence.DAL;
using Persistence.DAL.EntityFrameworkExtensions;
using Persistence.DAL.FetchStrategies;
using Persistence.DAL.FetchStrategies.ToursFetchStrategies;

namespace Services.Services
{
    public class ExpertService
    {
        private readonly IPredictionsContext _context;

        public ExpertService(IPredictionsContext context)
        {
            _context = context;
        }


        public IReadOnlyList<Expert> GetExperts()
        {
            return _context.GetExperts().ToList();
        }

        public IReadOnlyList<string> GenerateVenceslavaPredictions(int matchNumber)
        {
            var scores = new List<string>();
  
            for (var i = 1; i <= matchNumber; i++)
            {
                scores.Add("0:0");
            }

            return scores;

        }

        public IReadOnlyList<string> GenerateRandomizerPredictions(int matchNumber)
        {
            var scores = new List<string>();
            var r = new Random();
            for (var i = 1; i <= matchNumber; i++)
            {
                scores.Add(r.Next(0, 3).ToString() + ":" + r.Next(0, 3).ToString());
            }

            return scores;

        }

        public IReadOnlyList<string> GenerateMeanPredictions(int tourId)
        {
            var fetchStrategies = new IFetchStrategy<Tour>[]
            {
                new FetchMatchesWithPredictions()
            };
            var tour = _context.GetTours(fetchStrategies).AsNoTracking().Single(t => t.TourId == tourId);
            var matches = tour.Matches;
            var scores = new List<string>();


            foreach (var match in matches)
            {
                var homeSum = 0;
                var awaySum = 0;
                var matchNumber = matches.Count;
                foreach (var prediction in match.Predictions)
                {
                    homeSum += PredictionEvaluator.GetHomeGoals(prediction.Value);
                    awaySum += PredictionEvaluator.GetAwayGoals(prediction.Value);
                }

                var homeMean = homeSum / matchNumber;
                var awayMean = awaySum / matchNumber;
                scores.Add(homeMean + ":" + awayMean);
            }

            return scores;
        }
    }
}