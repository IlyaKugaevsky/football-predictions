using Predictions.ViewModels.Basis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Predictions.Models.Dtos;

namespace Predictions.ViewModels
{
    public class ResultsTableViewModel
    {
        public ResultsTableViewModel()
        { }

        public ResultsTableViewModel(List<NewTourDto> tours, List<ExpertDto> resultsTable)
        {
            Tourlist = GenerateSelectList(tours);
            ResultsTable = resultsTable;
        }

        public List<SelectListItem> Tourlist { get; set; }
        public int SelectedTourId { get; set; }

        public List<ExpertDto> ResultsTable { get; set; }

        private List<SelectListItem> GenerateSelectList(List<NewTourDto> tours)
        {
            var tourlist = new List<SelectListItem>()
            {
                new SelectListItem { Text = "За все туры", Value = "0" }
            };

            tours.ForEach(t => tourlist.Add(
                   new SelectListItem()
                   {
                       Text = t.TourNumber.ToString(),
                       Value = t.TourNumber.ToString()
                   }));

            return tourlist;
        }
    }
}