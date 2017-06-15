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
using Predictions.ViewModels.Basis;

namespace Predictions.Services
{
    public class TourService
    {
        private readonly PredictionsContext _context;

        //move to EF
        public int MatchesCount(int tourId)
        {
            return _context.Tours.Single(t => t.TourId == tourId).Matches.Count();
        }

        //move to EF
        public bool AllResultsReady(int tourId)
        {
            return !EagerLoad(tourId, t => t.Matches).Matches.Any(m => m.Score.IsNullOrEmpty());
        }

        public TourService(PredictionsContext context)
        {
            _context = context;
        }

        public TourInfo GetTourInfo(int? tourId)
        {
            if (tourId == null) return null;
            var tour = _context.Tours.Find(tourId);
            return new TourInfo(tour.TourId, tour.StartDate, tour.EndDate);
        }

        public void UpdateTour(TourInfo tourInfo)
        {
            var tour = _context.Tours.Find(tourInfo.TourId);
            if (tour != null)
            {
                tour.StartDate = tourInfo.StartDate;
                tour.EndDate = tourInfo.EndDate;
                _context.SaveChanges();
            }
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

        //public List<Tour> EagerLoad(params Expression<Func<Tour, object>>[] includes)
        //{
        //    return _context.Tours.IncludeMultiple(includes)
        //        .ToList();
        //}

        //public Tour LoadBasicsWith(int? id, params Expression<Func<Tour, object>>[] includes)
        //{
        //    if (id == null) return null;
        //    return _context.Tours
        //            .Include(t => t.Matches
        //                .Select(m => m.HomeTeam))
        //            .Include(t => t.Matches
        //                .Select(m => m.AwayTeam))
        //            .IncludeMultiple(includes)
        //            .ToList()
        //            .Single(t => t.TourId == id);
        //}

        public List<Tour> LoadBasicsWith(params Expression<Func<Tour, object>>[] includes)
        {
            //var tours = _context.Tournaments.Last().Tours;
            //       return tours.Include(t => t.Matches
            //            .Select(m => m.HomeTeam))
            //        .Include(t => t.Matches
            //            .Select(m => m.AwayTeam))
            //        .IncludeMultiple(includes)
            //        .ToList();


            //return _context.Tournaments.Include(trn => trn.Tours).Last().Tours;

            var trnm = _context.Tournaments.Find(1);
            var tours = trnm.Tours.ToList();

            return tours;
        }

        public List<Match> GetMatchesByTour(int? id)
        {
            if (id == null) return null;
            return EagerLoad(id, t => t.Matches).Matches.ToList();
        }

        //not sure
        public List<Tuple<Expert, int>> GenerateTourPreresultlist(int tourId)
        {
            var tour = EagerLoad(tourId, t => t.Matches.Select(m => m.Predictions));
            var matches = tour.Matches;
            var predictions = matches.SelectMany(m => m.Predictions).ToList();
            var experts = _context.Experts.ToList();

            var tourPreresultList = new List<Tuple<Expert, int>>();

            for (var i = 0; i < experts.Count(); i++)
            {
                tourPreresultList.Add(
                    new Tuple<Expert, int>(
                        experts[i], 
                        predictions.Count(p => p.ExpertId == experts[i].ExpertId 
                        && !string.IsNullOrEmpty(p.Value))));
            }
            return tourPreresultList;
        }
    }
}