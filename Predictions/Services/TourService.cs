using Predictions.DAL;
using Predictions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;


namespace Predictions.Services
{
    public class TourService
    {
        public List<SelectListItem> GenerateSelectList(PredictionsContext context)
        {
            var tourlist = new List<SelectListItem>();
            context.Tours.ToList().
                ForEach(t => tourlist.Add
                (
                    new SelectListItem() { Text = t.TourId.ToString(), Value = t.TourId.ToString() }
                ));
            return tourlist;
        }

        public Tour LoadMatchInfoWithPredictions(int id, PredictionsContext context)
        {
            var tour = context.Tours
                       .Include(t => t.Matches
                           .Select(m => m.HomeTeam))
                       .Include(t => t.Matches
                           .Select(m => m.AwayTeam))
                       .Include(t => t.Matches
                           .Select(m => m.Predictions))
                       .Single(t => t.TourId == id);
            return tour;
        }

    }
}