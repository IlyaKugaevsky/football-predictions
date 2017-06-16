using Predictions.ViewModels.Basis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Predictions.ViewModels
{
    public class ResultsTableViewModel
    {
        public ResultsTableViewModel()
        { }

        //public ResultsTableViewModel(List<SelectListItem> tourList, List<ExpertInfo> resultsTable)
        //{
        //    Tourlist = tourList;
        //    ResultsTable = resultsTable;
        //}

        public ResultsTableViewModel(List<TourInfo> tours, List<ExpertInfo> resultsTable)
        {
            Tourlist = GenerateSelectList(tours);
            ResultsTable = resultsTable;
        }

        public List<SelectListItem> Tourlist { get; set; }
        public int SelectedTourId { get; set; }

        public List<ExpertInfo> ResultsTable { get; set; }

        public List<SelectListItem> GenerateSelectList(List<TourInfo> tours)
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