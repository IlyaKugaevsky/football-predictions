using Predictions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Predictions.ViewModels
{
    public class AddPredictionViewModel
    {
        //display
        public List<Expert> Expertlist { get; set; }
        public Tour Tour { get; set; }

        //input
        public List<string> InputPredictionValuelist { get; set; }
        public int SelectedExpertId { get; set; }
    }
}