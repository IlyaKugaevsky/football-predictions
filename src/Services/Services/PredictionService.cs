using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Core.Models;
using Core.Models.Dtos;
using Core;
using Core.Helpers;
using Persistence.DAL;
using Persistence.DAL.EntityFrameworkExtensions;
using Persistence.DAL.FetchStrategies;
using Persistence.DAL.FetchStrategies.TournamentsFetchStrategies;
using Persistence.DAL.FetchStrategies.ToursFetchStrategies;

namespace Services.Services
{
    public class PredictionService
    {
        private readonly IPredictionsContext _context;

        public PredictionService(IPredictionsContext context)
        {
            _context = context;
        }

        //add custom includes
        public Tour LoadTour(int? tourId)
        {
            var fetchStrategies = new IFetchStrategy<Tour>[]
            {
                 new FetchMatchesWithPredictionsWithExperts()
            };

            return _context.GetTours(fetchStrategies).Single(t => t.TourId == tourId);
        }

        public List<FootballScoreViewModel> GeneratePredictionlist(int tourId, int expertId, bool editable = false, string emptyDisplay = "-")
        {
            var fetchStrategies = new IFetchStrategy<Tour>[]
            {
                 new FetchMatchesWithPredictionsWithExperts()
            };


            var tour = _context.Tours
                .Include(t => t.Matches.Select(m => m.Predictions.Select(p => p.Expert))).Single(t => t.TourId == tourId);

            var mpList = tour.Matches.Select(m => new
            {
                //match-prediction list
                Match = m,
                Prediction = m.Predictions.SingleOrDefault(p => p.ExpertId == expertId)
            })
            .ToList();

            emptyDisplay = editable ? string.Empty : emptyDisplay;

            return mpList
                .Select(mp => new FootballScoreViewModel(mp.Prediction == null ? emptyDisplay : mp.Prediction.Value, editable))
                .ToList();
        }

        //predictions can be null; need matches!
        public List<string> GenerateTempResultlist(int tourId, int expertId, string emptyDisplay = "-")
        {
            //var predictions = LoadTourPredictions(tourId, expertId);
            //if (predictions.IsNullOrEmpty()) return null;
            //return predictions.Select(p => p.IsClosed ? p.Sum.ToString() : "-").ToList();

            var tour = _context.Tours
                .Include(t => t.Matches.Select(m => m.Predictions.Select(p => p.Expert))).Single(t => t.TourId == tourId);

            var mpList = tour.Matches.Select(m => new
            {
                //match-prediction list
                Match = m,
                Prediction = m.Predictions.SingleOrDefault(p => p.ExpertId == expertId)
            })
            .ToList();

            return mpList
                .Select(mp => mp.Prediction?.Sum.ToString() ?? emptyDisplay)
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

            var matches = tours.Single(t => t.TourNumber == tourNumber)
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

        
        //decompose
        //why Football score? mb strings?
        public void AddExpertPredictions(int expertId, int tourId, IList<string> scorelist)
        {
            var tour = _context.Tours
                .Include(t => t.Matches.Select(m => m.Predictions)).Single(t => t.TourId == tourId);

            var mpList = tour.Matches.Select(m => new
            {
                //match-prediction list
                Match = m,
                Prediction = m.Predictions.SingleOrDefault(p => p.ExpertId == expertId)
            })
            .ToList();

            var createdPredictions = new List<Prediction>();
            for (var i = 0; i < mpList.Count(); i++)
            {
                if (mpList[i].Prediction == null) createdPredictions.Add(new Prediction(expertId, mpList[i].Match.MatchId, scorelist[i]));
                else mpList[i].Prediction.Value = scorelist[i];
            }
            _context.Predictions.AddRange(createdPredictions);
            _context.SaveChanges();
        }

        public void AddBotPredictions(int tourId)
        {
            var tour = _context.Tours
                .Include(t => t.Matches.Select(m => m.Predictions)).AsNoTracking().Single(t => t.TourId == tourId);

            var matches = tour.Matches;

            var meanScores = GenerateMeanPredictions(tourId);
            AddExpertPredictions(25, tourId, meanScores.ToList());

            //ORDER!
            var slavaScores = GenerateVenceslavaPredictions(matches.Count);
            var randomScores = GenerateRandomizerPredictions(matches.Count);

            AddExpertPredictions(16, tourId, slavaScores.ToList());
            if(matches.First().Predictions.Where(m => m.ExpertId == 24).IsNullOrEmpty()) AddExpertPredictions(24, tourId, randomScores.ToList());
        }

        //mb needs IPredictionEvaluator
        private void SubmitPrediction (Prediction prediction)
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

        public void SubmitTourPredictions(int? tourId)
        {
            var tour = LoadTour(tourId);
            var matches = tour.Matches.ToList();

            foreach(var m in matches)
            {
                if (m.Score.IsNullOrEmpty()) throw new ArgumentException("Match score is not set.");

                var predictions = m.Predictions.ToList();
                foreach(var p in predictions)
                {
                    if (!p.IsClosed) SubmitPrediction(p);
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
                scores.Add(r.Next(0, 4).ToString() + ":" + r.Next(0, 4).ToString());
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
                var predictionValues = match.Predictions.Select(p => p.Value);
                scores.Add(CalculateMeanScore(predictionValues));
            }

            return scores;
        }

        public string CalculateMeanScore(IEnumerable<string> scores)
        {
            var homeSum = 0;
            var awaySum = 0;
            var scoreList = scores.ToList();

            foreach (var score in scoreList)
            {
                if (!PredictionEvaluator.IsValidScore(score)) throw new ArgumentException("Not a valid score");
                homeSum += PredictionEvaluator.GetHomeGoals(score);
                awaySum += PredictionEvaluator.GetAwayGoals(score);
            }

            var homeMean = homeSum / scoreList.Count;
            var awayMean = awaySum / scoreList.Count;

            return homeMean + ":" + awayMean;
        }

    }
}