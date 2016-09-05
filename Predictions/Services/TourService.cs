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
        private readonly PredictionsContext _context;

        public TourService(PredictionsContext context)
        {
            _context = context;
        }

        public List<SelectListItem> GenerateSelectList()
        {
            var tourlist = new List<SelectListItem>();
            _context.Tours.ToList()
                .ForEach(t => tourlist.Add( new SelectListItem() { Text = t.TourId.ToString(), Value = t.TourId.ToString() }));
            return tourlist;
        }


        public Tour EagerLoad(int? id, params Expression<Func<Tour, object>>[] includes)
        {
            if (id == null) return null;
            return _context.Tours.IncludeMultiple(includes)
                .ToList()
                .Single(t => t.TourId == id);
        }

        public List<Tour> EagerLoad(params Expression<Func<Tour, object>>[] includes)
        {
            return _context.Tours.IncludeMultiple(includes)
                .ToList();
        }

        public Tour LoadBasicsWith(int? id, params Expression<Func<Tour, object>>[] includes)
        {
            if (id == null) return null;
            return _context.Tours
                    .Include(t => t.Matches
                        .Select(m => m.HomeTeam))
                    .Include(t => t.Matches
                        .Select(m => m.AwayTeam))
                    .IncludeMultiple(includes)
                    .ToList()
                    .Single(t => t.TourId == id);
        }

        public List<Tour> LoadBasicsWith(params Expression<Func<Tour, object>>[] includes)
        {
            return _context.Tours
                    .Include(t => t.Matches
                        .Select(m => m.HomeTeam))
                    .Include(t => t.Matches
                        .Select(m => m.AwayTeam))
                    .IncludeMultiple(includes)
                    .ToList();
        }

        public List<Match> GetMatchesByTour(int? id)
        {
            if (id == null) return null;
            return EagerLoad(id, t => t.Matches).Matches.ToList();
        }

    }
}