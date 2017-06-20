using Predictions.Models;
using Predictions.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Predictions.ViewModels
{
    public class PredictionsDisplayViewModel
    {
        public PredictionsDisplayViewModel()
        { }

        public PredictionsDisplayViewModel (List<SelectListItem> expertlist, List<Tour> tours, EvaluationDetailsViewModel evaluationDetails)
        {
            Expertlist = expertlist;
            Tourlist = GenerateSelectList(tours);
            EvaluationDetails = evaluationDetails;
        }

        [Required]
        public List<SelectListItem> Expertlist { get; set; }

        [Required]
        public List<SelectListItem> Tourlist { get; set; }

        public int SelectedExpertId { get; set; }
        public int SelectedTourId { get; set; }
        public EvaluationDetailsViewModel EvaluationDetails { get; set; }
        //public MatchTableViewModel MatchTable { get; set; }

        private List<SelectListItem> GenerateSelectList(List<Tour> tours)
        {
            return tours.Select(t => new SelectListItem()
            {
                Text = t.TourNumber.ToString(),
                Value = t.TourId.ToString()
            }).ToList();
        }

    }
}