using Predictions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Predictions.ViewModels
{
    public class AddPredictionsViewModel
    {
        //display
        public List<SelectListItem> Expertlist { get; set; }
        public Tour Tour { get; set; }

        //input
        public List<string> InputPredictionValuelist { get; set; }

        public List<MatchInfo> InputData { get; set; }

        public int SelectedExpertId { get; set; }
    }
}