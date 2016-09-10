using Predictions.Models;
using Predictions.ViewModels.Basis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Predictions.ViewModels
{
    public class EditTourViewModel
    {
        public EditTourViewModel()
        { }

        public EditTourViewModel(List<SelectListItem> teamlist, TourInfo tourInfo, MatchTableViewModel matchTable)
        {
            Teamlist = teamlist;
            TourInfo = tourInfo;
            MatchTable = matchTable;
        }

        public TourInfo TourInfo { get; set; }
        public MatchTableViewModel MatchTable { get; set; }
        //model to edit 
        // display (current values) + input (new values)
        //public Tour Tour { get; set; }

        //add new match
        //display
        //public List<Team> Teamlist { get; set; }

        public List<SelectListItem> Teamlist { get; set; }
        //input
        public int SelectedHomeTeamId { get; set; }
        public int SelectedAwayTeamId { get; set; }
        public DateTime InputDate { get; set; }
    }
}