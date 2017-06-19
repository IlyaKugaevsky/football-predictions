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
using Predictions.DAL.FetchStrategies;
using Predictions.DAL.FetchStrategies.TournamentFetchStrategies;
using Predictions.Models.Dtos;
using Predictions.ViewModels.Basis;

namespace Predictions.Services
{
    public class TourService
    {
        private readonly PredictionsContext _context;

        //move to EF
        public int MatchesCount(int tourId)
        {
            return _context.NewTours.Single(t => t.TourId == tourId).Matches.Count();
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

        public NewTourDto GetTourDto(int tourId)
        {
            return _context.NewTours.Single(t => t.TourId == tourId).GetDto();
        }

        public void UpdateTour(NewTourDto newTourDto)
        {
            var tour = _context.NewTours.Find(newTourDto.TourId);
            if (tour != null)
            {
                tour.StartDate = newTourDto.StartDate;
                tour.EndDate = newTourDto.EndDate;
                _context.SaveChanges();
            }
        }

        public List<SelectListItem> GenerateSelectList()
        {
            var tourlist = new List<SelectListItem>();
            _context.NewTours.ToList()
                .ForEach(t => tourlist.Add( new SelectListItem() { Text = t.TourId.ToString(), Value = t.TourId.ToString() }));
            return tourlist;
        }


        public Tour EagerLoad(int? id, params Expression<Func<Tour, object>>[] includes)
        {
            if (id == null) return null;
            return _context.NewTours.IncludeMultiple(includes)
                .ToList()
                .Single(t => t.TourId == id);
        }


        public List<Tour> GetLastTournamentSchedule()
        {
            var fetchStrategies = new IFetchStrategy<Tournament>[]
            {
                new ToursWithMatchesWithHomeTeam(),
                new ToursWithMatchesWithAwayTeam()
            };
            return _context.GetLastTournamentTours(fetchStrategies).ToList();
        }

        //public List<Tour> LoadBasicsWith(params Expression<Func<Tour, object>>[] includes)
        //{
        //    //var tours = _context.Tournaments.Last().Tours;
        //    //       return tours.Include(t => t.Matches
        //    //            .Select(m => m.HomeTeam))
        //    //        .Include(t => t.Matches
        //    //            .Select(m => m.AwayTeam))
        //    //        .IncludeMultiple(includes)
        //    //        .ToList();


        //    //return _context.Tournaments.Include(trn => trn.Tours).Last().Tours;

        //    var trnm = _context.Tournaments.Find(1);
        //    var tours = trnm.Tours.ToList();

        //    return tours;
        //}

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