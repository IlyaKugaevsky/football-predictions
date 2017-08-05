using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Predictions.Core.Services;
using Predictions.DAL;
using Predictions.ViewModels;
using Predictions.ViewModels.Basis;
using Services;

namespace Predictions.Controllers
{
    public class ExpertsController : Controller
    {
        private readonly PredictionsContext _context;
        private readonly ExpertService _expertService;
        private readonly TourService _tourService;
        private readonly PredictionService _predictionService;
        private readonly MatchService _matchService;
        private readonly TeamService _teamService;
        public ExpertsController()
        {
            _context = new PredictionsContext();
            _expertService = new ExpertService(_context);
            _tourService = new TourService(_context);
            _predictionService = new PredictionService(_context);
            _matchService = new MatchService(_context);
            _teamService = new TeamService(_context);
        }


        public ActionResult Statistics()
        {
            return View();
        }

        // GET: Experts
        public ActionResult Index()
        {
            var tours = _tourService.GetLastTournamentTours();

            var results = _predictionService.GenerateExpertsInfo();
            var resultsTable = new ResultsTableViewModel(tours.Select(t => t.GetDto()).ToList(), results);

            return View(resultsTable);
        }

        [HttpPost]
        public ActionResult GetResultsTable(int tourId)
        {
            return PartialView("ResultsTable", _predictionService.GenerateExpertsInfo(tourId));
        }
    }
}
