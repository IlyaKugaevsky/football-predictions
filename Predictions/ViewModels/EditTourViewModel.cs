using Predictions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Predictions.ViewModels
{
    public class EditTourViewModel
    {
        public Tour Tour { get; set; }

        public List<Team> Teamlist { get; set; }
        public List<Match> Matchlist { get; set; }

        //public int TourId { get; set; } //required?
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public DateTime Date { get; set; }
    }
}