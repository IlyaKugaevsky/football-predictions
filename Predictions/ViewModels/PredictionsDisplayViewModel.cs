using Predictions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Predictions.ViewModels
{
    public class PredictionsDisplayViewModel
    {
        public List<Expert> Expertlist { get; set; }
        public List<Match> Matchlist { get; set; }

        public int SelecetedExpertId { get; set; }
        public int SelectedTourId { get; set; }

        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public DateTime Date { get; set; }

    }
}