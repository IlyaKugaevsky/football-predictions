using Predictions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Predictions.ViewModels
{
    public class EditTourViewModel
    {
        //model to edit 
            // display (current values) + input (new values)
        public Tour Tour { get; set; }

        //add new match
            //display
        public List<Team> Teamlist { get; set; }
            //input
        public int SelectedHomeTeamId { get; set; }
        public int SelectedAwayTeamId { get; set; }
        public DateTime InputDate { get; set; }
    }
}