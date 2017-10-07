﻿using Web.ViewModels.Basis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Models.Dtos;
using Persistence.DAL.EntityFrameworkExtensions;
using Services.Helpers;
//using Predictions.DAL;
//using Predictions.DAL.EntityFrameworkExtensions;

namespace Web.ViewModels
{
    public class EvaluationDetailsViewModel
    {
        public EvaluationDetailsViewModel()
        {
        }

        public EvaluationDetailsViewModel(List<MatchDto> matchlist, IList<FootballScore> scorelist, List<FootballScore> predictionlist, List<string> tempResultlist)
        {
            Matchlist = matchlist ?? new List<MatchDto>();
            Scorelist = scorelist ?? new List<FootballScore>();
            Predictionlist = predictionlist ?? new List<FootballScore>();
            TempResultlist = tempResultlist ?? new List<string>();
            if (!GenericsHelper.IsNullOrEmpty(tempResultlist) && !tempResultlist.Contains("-")) Sum = tempResultlist.Select(tr => Convert.ToInt32(tr)).Sum();
        }
        public List<MatchDto> Matchlist { get; set; }
        public IList<FootballScore> Scorelist { get; set; }
        public List<FootballScore> Predictionlist { get; set; }
        public List<string> TempResultlist { get; set; }

        public int Sum = -1;
    }
}