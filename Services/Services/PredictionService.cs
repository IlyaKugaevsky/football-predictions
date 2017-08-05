using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Core.Models;
using Core.Models.Dtos;
using Core;
using Persistence.DAL;
using Persistence.DAL.EntityFrameworkExtensions;
using Persistence.DAL.FetchStrategies;
using Persistence.DAL.FetchStrategies.TournamentsFetchStrategies;

namespace Services.Services
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
            return Queryable.Single<Tour>(_context.Tours
                    .Include(t => Enumerable.Select<Match, IEnumerable<Expert>>(t.Matches, m => Enumerable.Select<Prediction, Expert>(m.Predictions, p => p.Expert))), t => t.TourId == tourId);
        }

        public List<FootballScore> GeneratePredictionlist(int tourId, int expertId, bool editable = false, string emptyDisplay = "-")
        {

           // var fetchStrategies = new IFetchStrategy<Tour>[]
           //{
           //     new MatchesWithPredictionsWIthExperts()
           //};

            var tour = Queryable.Single<Tour>(_context.Tours
                    .Include(t => Enumerable.Select<Match, IEnumerable<Expert>>(t.Matches, m => Enumerable.Select<Prediction, Expert>(m.Predictions, p => p.Expert))), t => t.TourId == tourId);

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

            var tour = Queryable.Single<Tour>(_context.Tours
                    .Include(t => Enumerable.Select<Match, IEnumerable<Expert>>(t.Matches, m => Enumerable.Select<Prediction, Expert>(m.Predictions, p => p.Expert))), t => t.TourId == tourId);

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
                var experts = Enumerable.ToList<Expert>(_context.Experts);
                experts.ForEach(e => results.Add(new ExpertDto(e.Nickname, e.Sum, e.Scores, e.Differences, e.Outcomes)));
                return results;
            }

            var fetchStrategies = new IFetchStrategy<Tournament>[]
            {
                new FetchToursWithMatchesWithPredictionsWithExperts()
            };

            var tours = _context.GetLastTournamentTours(fetchStrategies);

            var matches = Queryable.Single<Tour>(tours, t => t.TourNumber == tourNumber)
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
            var tour = Queryable.Single<Tour>(_context.Tours
                    .Include(t => Enumerable.Select<Match, List<Prediction>>(t.Matches, m => m.Predictions)), t => t.TourId == tourId);

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