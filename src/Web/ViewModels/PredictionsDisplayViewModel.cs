using Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Core.Models;

namespace Web.ViewModels
{
    public class PredictionsDisplayViewModel
    {
        public PredictionsDisplayViewModel()
        { }

        public PredictionsDisplayViewModel (IEnumerable<Expert> experts, List<Tour> tours, EvaluationDetailsViewModel evaluationDetails = null)
        {
            Expertlist = GenerateSelectList(experts);
            Tourlist = GenerateSelectList(tours);
            EvaluationDetails = evaluationDetails ?? new EvaluationDetailsViewModel();
        }

        [Required]
        public IReadOnlyList<SelectListItem> Expertlist { get; set; }

        [Required]
        public IReadOnlyList<SelectListItem> Tourlist { get; set; }

        public int SelectedExpertId { get; set; }
        public int SelectedTourId { get; set; }
        public EvaluationDetailsViewModel EvaluationDetails { get; set; }

        private IReadOnlyList<SelectListItem> GenerateSelectList(List<Tour> tours)
        {
            return tours.Select(t => new SelectListItem()
            {
                Text = t.TourNumber.ToString(),
                Value = t.TourId.ToString()
            }).ToList();
        }

        private IReadOnlyList<SelectListItem> GenerateSelectList(IEnumerable<Expert> experts)
        {
            return experts.Select(e => new SelectListItem()
            {
                Text = e.Nickname,
                Value = e.ExpertId.ToString()
            }).ToList();
        }

    }
}