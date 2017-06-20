using Predictions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Predictions.Models;
using Predictions.ViewModels;
using System.Net;
using System.Data.Entity;
using Predictions.Services;
using Predictions.ViewModels.Basis;

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
            var evaluationDetails = new EvaluationDetailsViewModel();
            var expertlist = _expertService.GenerateSelectList();
            //var tourlist = _tourService.GenerateSelectList();
            var tours = _tourService.GetLastTournamentTours();
            var viewModel = new PredictionsDisplayViewModel(expertlist, tours, evaluationDetails);
            return View(viewModel);
        }

        //bind, invalide model
        //[HttpPost]
        //public ActionResult PredictionsDisplay(PredictionsDisplayViewModel viewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var headers = new List<string>() { "Дата", "Дома", "В гостях", "Прогноз" };
        //        var matchlist = _matchService.GenerateMatchlist(viewModel.SelectedTourId);
        //        var scorelist = _predictionService.GeneratePredictionlist(viewModel.SelectedTourId, viewModel.SelectedExpertId);
        //        var matchtable = new MatchTableViewModel(headers, matchlist, scorelist);
        //        viewModel.EvaluationDetails = matchtable;
        //        return View(viewModel);
        //    }
        //    //wtf!
        //    else return HttpNotFound();
        //}

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
            var matchlist = _matchService.GetLastTournamentMatchesByTourId(tourId).Select(m => m.GetDto()).ToList();
            var scorelist = _matchService.GenerateScorelist(tourId);
            var predictionlist = _predictionService.GeneratePredictionlist(tourId, expertId);
            var tempResultlist = _predictionService.GenerateTempResultlist(tourId, expertId);
            var evaluationDetails = new EvaluationDetailsViewModel(matchlist, scorelist, predictionlist, tempResultlist);
            return PartialView("EvaluationDetails", evaluationDetails);
        }
    }
}