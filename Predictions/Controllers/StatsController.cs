using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Predictions.Core.Services;
//using Predictions.Core.Services;
using Predictions.DAL;
using Predictions.ViewModels;
using Services;

namespace Predictions.Controllers
{
    public class StatsController : Controller
    {
        private readonly PredictionsContext _context;
        private readonly ExpertService _expertService;
        private readonly TourService _tourService;
        private readonly PredictionService _predictionService;
        private readonly MatchService _matchService;
        private readonly TeamService _teamService;
        private readonly StatService _statService;

        public StatsController()
        {
            _context = new PredictionsContext();
            _expertService = new ExpertService(_context);
            _tourService = new TourService(_context);
            _predictionService = new PredictionService(_context);
            _matchService = new MatchService(_context);
            _teamService = new TeamService(_context);
            _statService = new StatService(_context);
        }

        public ActionResult MatchStats()
        {
            var tours = _tourService.GetLastTournamentTours().Select(t => t.GetDto()).ToList();
            var matchStats = _statService.GenerateMatchStats(tours.First().TourId);
            var matchStatsVieModel = new MatchStatsViewModel(tours, matchStats);
            return View(matchStatsVieModel);
        }

        [HttpPost]
        public ActionResult GetMatchStatsTable(int tourId)
        {
            return PartialView("MatchStatsTable", _statService.GenerateMatchStats(tourId));
        }

        public ActionResult ExpertsStats()
        {
            return View(_statService.GenerateExpertsOverallRating());
        }

        public ActionResult TopStats()
        {            
            return View(new TopStatsViewModel(_statService.GenerateTopStats()));
        }
    }
}