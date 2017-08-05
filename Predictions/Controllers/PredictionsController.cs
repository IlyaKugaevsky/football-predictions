using Predictions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Predictions.ViewModels;
using System.Net;
using System.Data.Entity;
using Predictions.ViewModels.Basis;
using Predictions.Core.Services;
using Services;

namespace Predictions.Controllers
{
    public class PredictionsController : Controller
    {
        private readonly PredictionsContext _context;
        private readonly ExpertService _expertService;
        private readonly TourService _tourService;
        private readonly PredictionService _predictionService;
        private readonly MatchService _matchService;

        public PredictionsController()
        {
            _context = new PredictionsContext();
            _expertService = new ExpertService(_context);
            _tourService = new TourService(_context);
            _predictionService = new PredictionService(_context);
            _matchService = new MatchService(_context);
        }

        public ActionResult PredictionsDisplay()
        {
            var experts = _expertService.GetExperts();
            var tours = _tourService.GetLastTournamentTours();
            var viewModel = new PredictionsDisplayViewModel(experts, tours);
            return View(viewModel);
        }

        //don't now how to encapsulate ViewModel stuff (like headers)
        [HttpPost]
        public ActionResult GetMatchTable(int tourId, int expertId)
        {
            var headers = new List<string>() { "Дата", "Дома", "В гостях", "Прогноз" };
            var matchlist = _matchService.GetLastTournamentMatchesByTourId(tourId).Select(m => m.GetDto()).ToList();
            var scorelist = _predictionService.GeneratePredictionlist(tourId, expertId);
            var matchTable = new MatchTableViewModel(headers, matchlist, scorelist);
            return PartialView("MatchTable", matchTable);
        }

        [HttpPost]
        public ActionResult GetEvaluationDetails(int tourId, int expertId)
        {
            var matches = _matchService.GetLastTournamentMatchesByTourId(tourId).Select(m => m.GetDto()).ToList();
            var scorelist = _matchService.GenerateScorelist(tourId);
            var predictionlist = _predictionService.GeneratePredictionlist(tourId, expertId);
            var tempResultlist = _predictionService.GenerateTempResultlist(tourId, expertId);
            var evaluationDetails = new EvaluationDetailsViewModel(matches, scorelist, predictionlist, tempResultlist);
            return PartialView("EvaluationDetails", evaluationDetails);
        }
    }
}