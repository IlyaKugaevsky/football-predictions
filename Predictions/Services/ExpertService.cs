using System;
using Predictions.DAL;
using Predictions.Models;
using Predictions.ViewModels.Basis;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Predictions.Services
{
    public class ExpertService
    {
        //public void AddPredictionResults(Expert expert, PredictionResults predictionResults,  PredictionsContext context)
        //{
        //    expert.Sum += predictionResults.Sum;
        //    if (predictionResults.Score) expert.Scores++;
        //    if (predictionResults.Difference) expert.Differences++;
        //    if (predictionResults.Outcome) expert.Outcomes++;
        //} 

        public List<SelectListItem> GenerateSelectList(PredictionsContext context)
        {
            var expertlist = new List<SelectListItem>();
            context.Experts.ToList().
                ForEach(e => expertlist.Add
                (
                    new SelectListItem() { Text = e.Nickname, Value = e.ExpertId.ToString() }
                ));
            return expertlist;
        }
    }
}