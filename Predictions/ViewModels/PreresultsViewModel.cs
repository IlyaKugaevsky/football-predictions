using Predictions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Predictions.ViewModels
{
    public class PreresultsViewModel
    {
        public PreresultsViewModel()
        { }

        public PreresultsViewModel(List<Tuple<Expert, int>> expertTourInfo, int tourId)
        {
            ExpertTourInfo = expertTourInfo.Select(
                e => new Tuple<string, string>(
                    e.Item1.Nickname, 
                    e.Item2.ToString()))
                    .ToList();
            CurrentTourId = tourId;
        }

        public int CurrentTourId { get; set; }

        public List<Tuple<string, string>> ExpertTourInfo { get; set; }
    }
}