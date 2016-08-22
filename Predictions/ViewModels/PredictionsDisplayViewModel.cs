using Predictions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Predictions.ViewModels
{
    public class PredictionsDisplayViewModel
    {
        //display
        public List<Expert> Expertlist { get; set; }
        public List<Tour> Tourlist { get; set; }

        public int SelectedExpertId { get; set; }
        public int SelectedTourId { get; set; }

        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public DateTime Date { get; set; }
    }
}