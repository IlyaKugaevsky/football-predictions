using Predictions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Predictions.ViewModels
{
    public class AddPredictionViewModel
    {
        public List<Match> Matchlist { get; set; }
        public List<Expert> Expertlist { get; set; }
        public List<string> PredictionValuelist { get; set; }

        public Tour Tour { get; set; }

        public int SelectedExpertId { get; set; }
    }
}