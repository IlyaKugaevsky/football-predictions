using System.Collections.Generic;
using System.Linq;
using Predictions.DAL;
using Predictions.Helpers;
using System.Data.Entity;
using Predictions.Core.Models;
using Predictions.DAL.EntityFrameworkExtensions;
using Predictions.DAL.FetchStrategies;
using Predictions.DAL.FetchStrategies.TournamentsFetchStrategies;
using Predictions.Core.Models.Dtos;

namespace Predictions.Core.Services
{
    public class PredictionService
    {
        private readonly PredictionsContext _context;

        public PredictionService(PredictionsContext context)
        {
            _context = context;
        }

        //add custom includes
        public Tour LoadTour(int? tourId)
        {
            return _context.Tours
                .Include(t => t.Matches
                    .Select(m => m.Predictions
                        .Select(p => p.Expert)))
                .Single(t => t.TourId == tourId);
        }

        public List<FootballScore> GeneratePredictionlist(int tourId, int expertId, bool editable = false, string emptyDisplay = "-")
        {

           // var fetchStrategies = new IFetchStrategy<Tour>[]
           //{
           //     new MatchesWithPredictionsWIthExperts()
           //};

            var tour = _context.Tours
                   .Include(t => t.Matches
                   .Select(m => m.Predictions
                   .Select(p => p.Expert)))
                   .Single(t => t.TourId == tourId);

            var mpList = Enumerable.Select(tour.Matches, m => new
            {
                //match-prediction list
                Match = m,
                Prediction = m.Predictions.SingleOrDefault(p => p.ExpertId == expertId)
            })
            .ToList();

            emptyDisplay = editable ? string.Empty : emptyDisplay;

            return mpList
                .Select(mp => new FootballScore(mp.Prediction == null ? emptyDisplay : mp.Prediction.Value, editable))
                .ToList();
        }

        //predictions can be null; need matches!
        public List<string> GenerateTempResultlist(int tourId, int expertId, string emptyDisplay = "-")
        {
            //var predictions = LoadTourPredictions(tourId, expertId);
            //if (predictions.IsNullOrEmpty()) return null;
            //return predictions.Select(p => p.IsClosed ? p.Sum.ToString() : "-").ToList();

            var tour = _context.Tours
                   .Include(t => t.Matches
                   .Select(m => m.Predictions
                   .Select(p => p.Expert)))
                   .Single(t => t.TourId == tourId);

            var mpList = Enumerable.Select(tour.Matches, m => new
            {
                //match-prediction list
                Match = m,
                Prediction = m.Predictions.SingleOrDefault(p => p.ExpertId == expertId)
            })
            .ToList();

            return mpList
                .Select(mp => mp.Prediction == null ? emptyDisplay : mp.Prediction.Sum.ToString())
                .ToList();
        }

        //optimization! looks ugly
        // "0" for all tours
        public List<ExpertDto> GenerateExpertsInfo(int tourNumber = 0)
        {
            var results = new List<ExpertDto>();

            if (tourNumber == 0)
            {
                var experts = _context.Experts.ToList();
                experts.ForEach(e => results.Add(new ExpertDto(e.Nickname, e.Sum, e.Scores, e.Differences, e.Outcomes)));
                return results;
            }

            var fetchStrategies = new IFetchStrategy<Tournament>[]
            {
                new FetchToursWithMatchesWithPredictionsWithExperts()
            };

            var tours = _context.GetLastTournamentTours(fetchStrategies);

            var matches = tours
                .Single(t => t.TourNumber == tourNumber)
                .Matches;

            var predictions = matches
                 .SelectMany(m => m.Predictions)
                 .GroupBy(p => p.Expert)
                 .ToList();

            foreach (var epGroup in predictions)
            {
                var info = new ExpertDto { Nickname = epGroup.Key.Nickname };
                foreach (var prediction in epGroup)
                {
                    info.Sum += prediction.Sum;
                    if (prediction.Score) info.Scores++;
                    else if (prediction.Difference) info.Differences++;
                    else if (prediction.Outcome) info.Outcomes++;
                }
                results.Add(info);
            }

            return results.OrderByDescending(expert => expert.Sum).ToList();
                
        }

        //public Prediction CreatePrediction(int expertId, int matchId, string value)
        //{
        //    return new Prediction() { ExpertId = expertId, MatchId = matchId, Value = value };
        //}

        
        //decompose
        //why Football score? mb strings?
        public void AddExpertPredictions(int expertId, int tourId, IList<FootballScore> scorelist)
        {
            var tour = _context.Tours
                  .Include(t => t.Matches
                  .Select(m => m.Predictions))
                  .Single(t => t.TourId == tourId);

            var mpList = Enumerable.Select(tour.Matches, m => new
            {
                //match-prediction list
                Match = m,
                Prediction = m.Predictions.SingleOrDefault(p => p.ExpertId == expertId)
            })
            .ToList();

            var createdPredictions = new List<Prediction>();
            for (var i = 0; i < mpList.Count(); i++)
            {
                if (mpList[i].Prediction == null) createdPredictions.Add(new Prediction(expertId, mpList[i].Match.MatchId, scorelist[i].Value));
                else mpList[i].Prediction.Value = scorelist[i].Value;
            }
            _context.Predictions.AddRange(createdPredictions);
            _context.SaveChanges();
        }

        //mb needs IPredictionEvaluator
        void SubmitPrediction (Prediction prediction)
        {
            var predictionResults = PredictionEvaluator.GetPredictionResults(prediction.Value, prediction.Match.Score);

            prediction.Sum = predictionResults.Sum;
            prediction.Score = predictionResults.Score;
            prediction.Difference = predictionResults.Difference;
            prediction.Outcome = predictionResults.Outcome;
           
            prediction.Expert.Sum += predictionResults.Sum;
            if (predictionResults.Score) prediction.Expert.Scores++;
            else if (predictionResults.Difference) prediction.Expert.Differences++;
            else if (predictionResults.Outcome) prediction.Expert.Outcomes++;

            prediction.IsClosed = true;
        }

        //mb fix
        public void SubmitTourPredictions(int? tourId)
        {
            var tour = LoadTour(tourId);
            foreach(var m in tour.Matches)
            {
                foreach(var p in m.Predictions)
                {
                    if(!p.IsClosed)SubmitPrediction(p);
                }
            }
            tour.IsClosed = true;
            _context.SaveChanges();
        }

        public void RestartTour(int tourId)
        {
            var tour = LoadTour(tourId);
            foreach (var m in tour.Matches)
            {
                foreach (var p in m.Predictions)
                {
                    if (p.IsClosed) RestartPrediction(p);
                }
            }
            tour.IsClosed = false;
            _context.SaveChanges();
        }

        private void RestartPrediction(Prediction prediction)
        {
            if (!prediction.IsClosed) return;
            prediction.Expert.Sum -= prediction.Sum;
            if (prediction.Score) prediction.Expert.Scores--;
            else if(prediction.Difference) prediction.Expert.Differences--;
            else if(prediction.Outcome) prediction.Expert.Outcomes--;

            prediction.Sum = 0;
            prediction.Score = false;
            prediction.Difference = false;
            prediction.Outcome = false;

            prediction.IsClosed = false;
        }
    }
}