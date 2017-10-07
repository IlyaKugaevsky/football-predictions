using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Models;

namespace Web.ViewModels
{
    public class PreresultsViewModel
    {
        public PreresultsViewModel()
        { }

        public PreresultsViewModel(List<Tuple<Expert, int>> expertTourInfo, int predictionsNeed, int tourId, bool enableSubmit)
        {
            ExpertTourInfo = expertTourInfo.Select(
                e => new Tuple<string, string>(
                    e.Item1.Nickname, 
                    e.Item2.ToString() + " из " + predictionsNeed.ToString()))
                    .ToList();
            CurrentTourId = tourId;
            EnableSubmit = enableSubmit;
        }

        public bool EnableSubmit { get; set; }
        public int CurrentTourId { get; set; }

        public List<Tuple<string, string>> ExpertTourInfo { get; set; }
    }
}