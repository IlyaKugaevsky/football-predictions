using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Predictions.DAL;
using Predictions.Models;
using Predictions.Helpers;
using System.Data.Entity;

namespace Predictions.Services
{
    public class PredictionService
    {
        public Tour LoadTour(int tourId, PredictionsContext context)
        {
            return context.Tours
                   .Include(t => t.Matches
                       .Select(m => m.Predictions
                           .Select(p => p.Expert)))
                   .SingleOrDefault(t => t.TourId == tourId);
        }

        public void SubmitPrediction (Prediction prediction, PredictionsContext context)
        {
            var predictionResults = PredictionEvaluator.GetPredictionResults(prediction.Value, prediction.Match.Score);
            prediction.Sum = predictionResults.Item1;
            prediction.Score = predictionResults.Item2;
            prediction.Difference = predictionResults.Item3;
            prediction.Outcome = predictionResults.Item4;
            prediction.Expert.Sum += predictionResults.Item1;
            prediction.IsClosed = true;
        }

        public void SubmitTourPredictions(int tourId, PredictionsContext context)
        {
            var tour = LoadTour(tourId, context);
            foreach(var m in tour.Matches)
            {
                foreach(var p in m.Predictions)
                {
                    if(!p.IsClosed)SubmitPrediction(p, context);
                }
            }
        }

        //public void RestartPrediction(Prediction prediction, context)
        //{

        //    prediction.Expert.Sum -= prediction.Sum;

        //    prediction.Sum = predictionResults.Item1;
        //    prediction.Score = predictionResults.Item2;
        //    prediction.Difference = predictionResults.Item3;
        //    prediction.Outcome = predictionResults.Item4;
        //    prediction.IsClosed = true;
        //}
    }
}