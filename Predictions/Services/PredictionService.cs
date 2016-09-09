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

        public Tour LoadTour(int? tourId)
        {
            return _context.Tours
                   .Include(t => t.Matches
                       .Select(m => m.Predictions))
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

        //public bool HasPredictionLoaded(Match match, int expertId)
        //{
        //    return !match.Predictions.Where(p => p.ExpertId == expertId).IsNullOrEmpty();
        //}

        public List<FootballScore> GenerateStraightScorelist(int length, string value = "-", bool editable = false)
        {
            var scorelist = new List<FootballScore>();
            for (var i = 0; i < length; i++)
            {
                scorelist.Add(new FootballScore(value, editable));
            }
            return scorelist;
        }

        //if single match has no predictions?
        //public List<FootballScore> GeneratePredictionlist(List<Match> matches, int? expertId = null, bool editable = false, string emptyDisplay = "-")
        //{
        //    if (expertId == null) return GenerateStraightScorelist(matches.Count, String.Empty, false);

        //    var predictions = LoadPredictions(matches, expertId);
        //    if (predictions == null) return null;

        //    return predictions.Select(p => new FootballScore
        //    {
        //        Value = (String.IsNullOrEmpty(p.Value) && editable == false) ? emptyDisplay : p.Value,
        //        Editable = editable
        //    }).ToList();
        //}

        public List<FootballScore> GeneratePredictionlist(int? tourId, int? expertId = null, bool editable = false, string emptyDisplay = "-")
        {
            if (tourId == null) return null;
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

            if (expertId == null) return GenerateStraightScorelist(mpList.Count, emptyDisplay, false);
            emptyDisplay = editable ? String.Empty : emptyDisplay;

            return mpList
                .Select(mp => new FootballScore(mp.Prediction == null ? emptyDisplay : mp.Prediction.Value, editable))
                .ToList();


            //else
            //{
            //    var predictions = matches.SelectMany(m => m.Predictions).Where(p => p.ExpertId == expertId).ToList();
            //    if (predictions.IsNullOrEmpty()) return GenerateStraightScorelist(matches.Count, editable ? String.Empty: emptyDisplay, editable);

            //    if (predictions.Count() == matches.Count())
            //    {
            //        return predictions.Select(p => new FootballScore(p.Value, editable)).ToList();

            //    }
            //    else
            //    {
            //        var scorelist = new List<FootballScore>();
            //        var j = 0;
            //        for(var i = 0; i < matches.Count(); i++)
            //        {
            //            // if has, but another Id?
            //            if(HasPredictionLoaded(matches[i], expertId.Value))
            //            {
            //                scorelist.Add(new FootballScore(predictions[j].Value, editable));
            //                j++;
            //            }
            //            else
            //            {
            //                scorelist.Add(new FootballScore(editable ? String.Empty : emptyDisplay, editable));
            //            }
            //        }
            //        return scorelist;
            //    }
            //}
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



            //very ugly, fix later
            //var tour = LoadTour(tourId);
            //var matches = tour.Matches.ToList();
            //var predictions = matches.SelectMany(m => m.Predictions).Where(p => p.ExpertId == expertId).ToList();
            //if (predictions.IsNullOrEmpty()) predictions = new List<Prediction>();
            //var j = 0;
            //var createdPredictions = new List<Prediction>();
            //for (var i = 0; i < matches.Count(); i++)
            //{
            //    // if has, but another Id?
            //    if (HasPredictionLoaded(matches[i], expertId))
            //    {
            //        matches[i].Predictions.Single(p => p.ExpertId == expertId).Value = predictions[j].Value;
            //        j++;
            //    }
            //    else
            //    {
            //        CreatePrediction(expertId, matches[i].MatchId, scorelist)
            //    }
            //}

            ////predictions.ForEach(p => _context.Predictions.Add(p));
            //_context.Predictions.AddRange(predictions);
            //_context.SaveChanges();
        }

        public void SubmitPrediction (Prediction prediction)
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
        }

        public void RestartPrediction(Prediction prediction)
        {
            if(prediction.IsClosed)
            {
                prediction.Expert.Sum -= prediction.Sum;
                if (prediction.Score) prediction.Expert.Scores++;
                if (prediction.Difference) prediction.Expert.Differences++;
                if (prediction.Outcome) prediction.Expert.Outcomes++;

                prediction.Sum = 0;
                prediction.Score = false;
                prediction.Difference = false;
                prediction.Outcome = false;

                prediction.IsClosed = false;
            }
        }
    }
}