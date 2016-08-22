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

        public Tour Tour { get; set; } //tour + matches + teams + predictions

        public int SelectedExpertId { get; set; }
        public int SelectedTourId { get; set; }
    }
}