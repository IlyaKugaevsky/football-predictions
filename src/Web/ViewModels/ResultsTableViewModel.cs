using Web.ViewModels.Basis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Models.Dtos;

namespace Web.ViewModels
{
    public class ResultsTableViewModel
    {
        public ResultsTableViewModel()
        { }

        public ResultsTableViewModel(List<TourDto> tours, List<ExpertDto> resultsTable)
        {
            Tourlist = GenerateSelectList(tours);
            ResultsTable = resultsTable;
        }

        public List<SelectListItem> Tourlist { get; set; }
        public int SelectedTourId { get; set; }

        public List<ExpertDto> ResultsTable { get; set; }

        private List<SelectListItem> GenerateSelectList(List<TourDto> tours)
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