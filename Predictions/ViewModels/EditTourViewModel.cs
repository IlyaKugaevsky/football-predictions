using Predictions.Models;
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

        public EditTourViewModel(List<SelectListItem> teamlist, Tour tour)
        {
            Teamlist = teamlist;
            Tour = tour;
        }

        //model to edit 
            // display (current values) + input (new values)
        public Tour Tour { get; set; }

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