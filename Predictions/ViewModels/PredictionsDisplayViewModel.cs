using Predictions.Models;
using Predictions.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Predictions.ViewModels
{
    public class PredictionsDisplayViewModel
    {
        //display
        public List<Expert> Expertlist { get; set; }
        public List<Tour> Tourlist { get; set; }
        public List<MatchInfo> Matchlist { get; set; }

        //input
        public int SelectedExpertId { get; set; }
        public int SelectedTourId { get; set; }
    }
}