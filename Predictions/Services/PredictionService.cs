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

        public Tour LoadTour(int tourId)
        {
            return _context.Tours
                   .Include(t => t.Matches
                       .Select(m => m.Predictions
                           .Select(p => p.Expert)))
                   .SingleOrDefault(t => t.TourId == tourId);
        }

        public Prediction CreatePrediction(int expertId, int matchId, string value)
        {
            return new Prediction() { ExpertId = expertId, MatchId = matchId, Value = value };
        }


        public void AddExpertPredictions(int expertId, List<Match> matches, List<FootballScore> scorelist)
        {
            var predictions = new List<Prediction>();
            for (var i = 0; i < matches.Count; i++)
            {
                predictions.Add(CreatePrediction(expertId, matches[i].MatchId, scorelist[i].Value));
            }
            predictions.ForEach(p => _context.Predictions.Add(p));
            _context.SaveChanges();
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

        public void SubmitTourPredictions(int tourId)
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