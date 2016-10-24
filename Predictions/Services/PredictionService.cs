using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Predictions.DAL;
using Predictions.Models;
using Predictions.Helpers;
using System.Data.Entity;
using Predictions.Services;
using Predictions.ViewModels.Basis;

namespace Predictions.Services
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


        //same order as matches, because of include
        public List<Prediction> LoadPredictions(List<Match> matches, int? expertId)
        {
            if (expertId == null || !matches.Any()) return null;
            var matchesIds = matches
                .Select(m => m.MatchId);

            matches = _context.Matches
                .Where(m => matchesIds.Contains(m.MatchId))
                .Include(m => m.Predictions)
                .ToList();

            return matches
                .SelectMany(m => m.Predictions)
                .Where(p => p.ExpertId == expertId)
                .ToList();
        }

        //same order as matches, because of include
        public List<Prediction> LoadTourPredictions(int? tourId, int? expertId)
        {
            if (expertId == null || tourId == null) return null;

            var matches = LoadTour(tourId).Matches;

            return matches
                .SelectMany(m => m.Predictions)
                .Where(p => p.ExpertId == expertId)
                .ToList();
        }

        public List<FootballScore> GenerateStraightScorelist(int length, string value = "-", bool editable = false)
        {
            var scorelist = new List<FootballScore>();
            for (var i = 0; i < length; i++)
            {
                scorelist.Add(new FootballScore(value, editable));
            }
            return scorelist;
        }

        public List<FootballScore> GeneratePredictionlist(int? tourId, int? expertId = null, bool editable = false, string emptyDisplay = "-")
        {
            if (tourId == null || expertId == null) return null;
            var tour = _context.Tours
                   .Include(t => t.Matches
                   .Select(m => m.Predictions
                   .Select(p => p.Expert)))
                   .Single(t => t.TourId == tourId);

            var mpList = tour.Matches.Select(m => new
            {
                //match-prediction list
                Match = m,
                Prediction = m.Predictions.SingleOrDefault(p => p.ExpertId == expertId)
            })
            .ToList();

            //if (expertId == null) return GenerateStraightScorelist(mpList.Count, emptyDisplay, false);
            emptyDisplay = editable ? String.Empty : emptyDisplay;

            return mpList
                .Select(mp => new FootballScore(mp.Prediction == null ? emptyDisplay : mp.Prediction.Value, editable))
                .ToList();
        }

        //predictions can be null; need matches!
        public List<string> GenerateTempResultlist(int? tourId, int? expertId = null, string emptyDisplay = "-")
        {
            //var predictions = LoadTourPredictions(tourId, expertId);
            //if (predictions.IsNullOrEmpty()) return null;
            //return predictions.Select(p => p.IsClosed ? p.Sum.ToString() : "-").ToList();

            if (tourId == null || expertId == null) return null;
            var tour = _context.Tours
                   .Include(t => t.Matches
                   .Select(m => m.Predictions
                   .Select(p => p.Expert)))
                   .Single(t => t.TourId == tourId);

            var mpList = tour.Matches.Select(m => new
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
        public List<ExpertInfo> GenerateExpertsInfo(int tourId)
        {
            var results = new List<ExpertInfo>();

            if (tourId == 0)
            {
                var experts = _context.Experts.ToList();
                experts.ForEach(e => results.Add(new ExpertInfo(e.Nickname, e.Sum, e.Scores, e.Differences, e.Outcomes)));
                return results;
            }

            var predictions = _context.Tours
                 .Include(t => t.Matches
                 .Select(m => m.Predictions
                 .Select(p => p.Expert)))
                 .Single(t => t.TourId == tourId)
                 .Matches
                 .SelectMany(m => m.Predictions)
                 .GroupBy(p => p.Expert)
                 .ToList();

            foreach (IGrouping<Expert, Prediction> epGroup in predictions)
            {
                var info = new ExpertInfo();
                info.Nickname = epGroup.Key.Nickname;
                foreach (var prediction in epGroup)
                {
                    info.Sum += prediction.Sum;
                    if (prediction.Score) info.Scores++;
                    else if (prediction.Difference) info.Differences++;
                    else if (prediction.Outcome) info.Outcomes++;
                }
                results.Add(info);
            }

            return results;
                
        }

        public Prediction CreatePrediction(int expertId, int matchId, string value)
        {
            return new Prediction() { ExpertId = expertId, MatchId = matchId, Value = value };
        }


        public void AddExpertPredictions(int expertId, int tourId, List<FootballScore> scorelist)
        {
            var tour = _context.Tours
                  .Include(t => t.Matches
                  .Select(m => m.Predictions))
                  .Single(t => t.TourId == tourId);

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
                if (mpList[i].Prediction == null) createdPredictions.Add(CreatePrediction(expertId, mpList[i].Match.MatchId, scorelist[i].Value));
                else mpList[i].Prediction.Value = scorelist[i].Value;
            }
            _context.Predictions.AddRange(createdPredictions);
            _context.SaveChanges();
        }

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

        void RestartPrediction(Prediction prediction)
        {
            if(prediction.IsClosed)
            {
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
    }
}