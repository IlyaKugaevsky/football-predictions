using Predictions.Models;
using Predictions.ViewModels.Basis;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace Predictions.ViewModels
{
    public class EditPredictionsViewModel
    {
        public EditPredictionsViewModel()
        { }

        public EditPredictionsViewModel(List<SelectListItem> expertlist, TourInfo tourInfo, MatchTableViewModel matchTable /*List<MatchInfo> matchlist, List<FootballScore> editPredictionsValuelist = null*/)
        {
            TourInfo = tourInfo;
            Expertlist = expertlist;
            MatchTable = matchTable;
            SubmitTextArea = new SubmitTextAreaViewModel();
        }

        //display
        public TourInfo TourInfo { get; set; }

        public List<SelectListItem> Expertlist { get; set; }

        public MatchTableViewModel MatchTable { get; set; }

        //input
        public int SelectedExpertId { get; set; }

        public SubmitTextAreaViewModel SubmitTextArea { get; set; }

        public bool AddPredictionsSuccess { get; set; } = false;
    }
}