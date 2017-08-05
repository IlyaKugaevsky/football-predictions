using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Predictions.Core.Models.Dtos;
using Predictions.ViewModels.Basis;

namespace Predictions.ViewModels
{
    public class MatchStatsViewModel
    {
        public MatchStatsViewModel()
        { }

        public MatchStatsViewModel(List<TourDto> tours, List<MatchStat> matchStats)
        {
            Tourlist = GenerateSelectList(tours);
            MatchStats = matchStats;
            SelectedTourId = tours.First().TourId;
        }

        public List<SelectListItem> Tourlist { get; set; }
        public int SelectedTourId { get; set; }

        public List<MatchStat> MatchStats { get; set; }

        private List<SelectListItem> GenerateSelectList(List<TourDto> tours)
        {
            var tourlist = new List<SelectListItem>();

            tours.ForEach(t => tourlist.Add(
                   new SelectListItem()
                   {
                       Text = t.TourNumber.ToString(),
                       Value = t.TourId.ToString()
                   }));

            return tourlist;
        }
    }
}