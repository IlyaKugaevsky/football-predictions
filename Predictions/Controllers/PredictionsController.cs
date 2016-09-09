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
            var headers = new List<string>() { "Дата", "Дома", "В гостях", "Прогноз" };
            var matchtable = new MatchTableViewModel(headers);
            var expertlist = _expertService.GenerateSelectList();
            var tourlist = _tourService.GenerateSelectList();
            var viewModel = new PredictionsDisplayViewModel(expertlist, tourlist, matchtable);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult PredictionsDisplay(PredictionsDisplayViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var headers = new List<string>() { "Дата", "Дома", "В гостях", "Прогноз" };
                var matchlist = _matchService.GenerateMatchlist(viewModel.SelectedTourId);
                var scorelist = _predictionService.GeneratePredictionlist(viewModel.SelectedTourId, viewModel.SelectedExpertId);
                var matchtable = new MatchTableViewModel(headers, matchlist, scorelist);
                viewModel.MatchTable = matchtable;
                return View(viewModel);

                //var tour = _tourService.LoadBasicsWith(viewModel.SelectedTourId, t => t.Matches.Select(m => m.Predictions));

                ////mb not so effective



                //if (tour == null) return HttpNotFound();

                //viewModel.MatchTable.Matchlist = new List<MatchInfo>();

                //for (var i = 0; i <= tour.Matches.Count() - 1; i++)
                //{
                //    viewModel.MatchTable.Matchlist.Add
                //    (
                //        new MatchInfo(tour.Matches[i].Date, tour.Matches[i].HomeTeam.Title, tour.Matches[i].AwayTeam.Title)
                //    );

                //    var filteredPredictions = tour.Matches[i].Predictions
                //        .Where(p => p.ExpertId == viewModel.SelectedExpertId).ToList();

                //just change to smth like predictionlist 

                //if (filteredPredictions.Count == 0 || filteredPredictions == null) viewModel.Matchlist[i].PredictionValue = "N/A";
                //else viewModel.Matchlist[i].PredictionValue = filteredPredictions.Count == 1 ? filteredPredictions[0].Value : "несколько";
            }
            else return HttpNotFound();

        }
        //WTF!!!
    }
}