using Predictions.DAL;
using Predictions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Net;

namespace Predictions.Services
{
    public class TourService
    {
        public List<SelectListItem> GenerateSelectList(PredictionsContext context)
        {
            var tourlist = new List<SelectListItem>();
            context.Tours.ToList()
                .ForEach(t => tourlist.Add( new SelectListItem() { Text = t.TourId.ToString(), Value = t.TourId.ToString() }));
            return tourlist;
        }


        public Tour EagerLoad(int id, PredictionsContext context, params Expression<Func<Tour, object>>[] includes)
        {
            return context.Tours.IncludeMultiple(includes)
                .ToList()
                .Single(t => t.TourId == id);
        }

        public IEnumerable<Tour> EagerLoad(PredictionsContext context, params Expression<Func<Tour, object>>[] includes)
        {
            return context.Tours.IncludeMultiple(includes)
                .ToList();
        }

        public Tour LoadBasicsWith(int? id, PredictionsContext context, params Expression<Func<Tour, object>>[] includes)
        {
            if (id == null) return null;
            return context.Tours
                    .Include(t => t.Matches
                        .Select(m => m.HomeTeam))
                    .Include(t => t.Matches
                        .Select(m => m.AwayTeam))
                    .IncludeMultiple(includes)
                    .ToList()
                    .Single(t => t.TourId == id);
        }

        public IEnumerable<Tour> LoadBasicsWith(PredictionsContext context, params Expression<Func<Tour, object>>[] includes)
        {
            return context.Tours
                    .Include(t => t.Matches
                        .Select(m => m.HomeTeam))
                    .Include(t => t.Matches
                        .Select(m => m.AwayTeam))
                    .IncludeMultiple(includes)
                    .ToList();
        }

    }
}