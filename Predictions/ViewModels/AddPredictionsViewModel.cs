using Predictions.Models;
using Predictions.ViewModels.Basis;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace Predictions.ViewModels
{
    public class AddPredictionsViewModel
    {
        public AddPredictionsViewModel()
        { }

        public AddPredictionsViewModel(List<SelectListItem> expertlist, TourInfo tourInfo, MatchTableViewModel matchTable /*List<MatchInfo> matchlist, List<FootballScore> editPredictionsValuelist = null*/)
        {
            TourInfo = tourInfo;
            Expertlist = expertlist;
            MatchTable = matchTable;
            //Matchlist = matchlist;
            //EditPredictionsValuelist = editPredictionsValuelist;
        }

        //display
        public TourInfo TourInfo { get; set; }

        public List<SelectListItem> Expertlist { get; set; }

        public MatchTableViewModel MatchTable { get; set; }

        //input
        public int SelectedExpertId { get; set; }
        //public Tour Tour { get; set; }

        //public List<MatchInfo> Matchlist { get; set; }

        ////input and display
        //public List<FootballScore> EditPredictionsValuelist { get; set; }

    }
}