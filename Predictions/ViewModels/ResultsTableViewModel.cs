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

        public ResultsTableViewModel(List<SelectListItem> tourList, List<ExpertInfo> resultsTable)
        {
            Tourlist = tourList;
            ResultsTable = resultsTable;
        }

        public List<SelectListItem> Tourlist { get; set; }
        public int SelectedTourId { get; set; }

        public List<ExpertInfo> ResultsTable { get; set; }

    }
}