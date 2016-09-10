using Predictions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Predictions.Models;
using Predictions.ViewModels;
using Predictions.Services;
using System.Net;
using System.Data.Entity;
using Predictions.ViewModels.Basis;
using Predictions.Helpers;

namespace Predictions.Controllers
{
    public class ToursController : Controller
    {
        private readonly PredictionsContext _context;
        private readonly ExpertService _expertService;
        private readonly TourService _tourService;
        private readonly PredictionService _predictionService;
        private readonly MatchService _matchService;
        private readonly TeamService _teamService;

        public ToursController()
        {
            _context = new PredictionsContext();
            _expertService = new ExpertService(_context);
            _tourService = new TourService(_context);
            _predictionService = new PredictionService(_context);
            _matchService = new MatchService(_context);
            _teamService = new TeamService(_context);
        }

        public ActionResult Index()
        {
            var tours = _tourService.LoadBasicsWith();
            if (tours == null) return HttpNotFound();
            return View(tours);

            //var t1 = DateTime.Now;
            //var t2 = DateTime.Now;
            //var time1 = DateTime.Now - t1;
            //var time2 = DateTime.Now - t2;

            //using (var context = new PredictionsContext())
            //{
            //    t1 = DateTime.Now;
            //    var tour = context.Tours
            //            .Include(t => t.Matches
            //                .Select(m => m.HomeTeam))
            //            .Include(t => t.Matches
            //                .Select(m => m.AwayTeam))
            //            .ToList()
            //            .Single(t => t.TourId == 1);
            //    time1 = DateTime.Now - t1;
            //}

            //using (var context = new PredictionsContext())
            //{

            //    t2 = DateTime.Now;
            //    var matches = context.Matches
            //        .Include(m => m.HomeTeam)
            //        .Include(m => m.AwayTeam)
            //        .Where(m => m.TourId == 1)
            //        .ToList();
            //    time2 = DateTime.Now - t2;

            //}

            //var delta = time1 - time2;
            //return null;
        }

        //404 after deleting
        public ActionResult EditTour(int? tourId)
        {
            //doesn't need
            Tour tour = _tourService.LoadBasicsWith(tourId);
            if (tour == null) return HttpNotFound();
            var tourInfo = _tourService.GetTourInfo(tourId);
            var teamlist = _teamService.GenerateSelectList();
            var headers = new List<string>() { "Дата", "Дома", "В гостях", "Счет" };
            var matchlist = _matchService.GenerateMatchlist(tourId);
            var scorelist = _matchService.GenerateScorelist(tourId);

            var actionLinklist = new List<ActionLinkParams>();
            var matches = _tourService.GetMatchesByTour(tourId);
            for (var i = 0; i < matches.Count; i++)
            {
                var actionLink = new ActionLinkParams("Удалить", "DeleteMatch", null, new { id = matches[i].MatchId }, new { @class = "btn btn-default" });
                actionLinklist.Add(actionLink);
            }

            var matchTable = new MatchTableViewModel(headers, matchlist, scorelist, actionLinklist);

            return View(new EditTourViewModel(teamlist, tourInfo, matchTable));
        }

        //bind include
        //TODO: real tour update
        [HttpPost]
        public ActionResult EditTour(EditTourViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var match = _matchService.CreateMatch(
                    viewModel.InputDate,
                    viewModel.SelectedHomeTeamId,
                    viewModel.SelectedAwayTeamId,
                    viewModel.TourInfo.TourId);
                _matchService.AddMatch(match);
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        public ActionResult AddPredictions(int? tourId, int? expertId)
        {
            if (tourId == null) return HttpNotFound();
            var expertlist = _expertService.GenerateSelectList();
            var tourInfo = _tourService.GetTourInfo(tourId);

            var headers = new List<string>() { "Дата", "Дома", "В гостях", "Прогноз" };
            var matchlist = _matchService.GenerateMatchlist(tourId);
            var scorelist = _predictionService.GeneratePredictionlist(tourId, expertId, true);

            var matchTable = new MatchTableViewModel(headers, matchlist, scorelist);
            var viewModel = new AddPredictionsViewModel(expertlist, tourInfo, matchTable);
            viewModel.SelectedExpertId = expertId ?? 0;
            return View(viewModel);
        }

        //bind
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "ShowPredictions")]
        public ActionResult ShowPredictions(AddPredictionsViewModel viewModel)
        {
            return RedirectToAction("AddPredictions", new { tourId = viewModel.TourInfo.TourId, expertId = viewModel.SelectedExpertId });
        }

        //bind
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "AddPredictions")]
        public ActionResult AddPredictions(AddPredictionsViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _predictionService.AddExpertPredictions(viewModel.SelectedExpertId, viewModel.TourInfo.TourId, viewModel.MatchTable.Scorelist);
                return RedirectToAction("AddPredictions", new { tourId = viewModel.TourInfo.TourId, expertId = viewModel.SelectedExpertId });
            }
            return View(viewModel);

        }

        public ActionResult AddScores(int? tourId)
        {
            var matchlist = _matchService.GenerateMatchlist(tourId);
            var scorelist = _matchService.GenerateScorelist(tourId, true);
            if (matchlist == null || scorelist == null) return HttpNotFound();
            var headers = new List<string>() { "Дата", "Дома", "В гостях", "Счет" };
            var matchTable = new MatchTableViewModel(headers, matchlist, scorelist);

            return View(new AddScoresViewModel(tourId.Value, matchTable));
        }

        [HttpPost]
        public ActionResult AddScores([Bind(Include = "MatchTable, CurrentTourId")] AddScoresViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _matchService.AddScores(_tourService.GetMatchesByTour(viewModel.CurrentTourId), viewModel.MatchTable.Scorelist);
                return RedirectToAction("Index");
            }
            return AddScores(viewModel.CurrentTourId); //not sure
        }

        public ActionResult SubmitTourPredictions(int tourId)
        {
            _predictionService.SubmitTourPredictions(tourId);
            return RedirectToAction("Index");
        }

        //404
        public ActionResult DeleteMatch(int? id)
        {
            //so~so, mb better
            var tourId = _matchService.GetTourId(id);
            if (tourId == null) return HttpNotFound(); //this also checks id
            _matchService.DeleteMatch(id);
            return RedirectToAction("EditTour", new { id = tourId });
        }

        //public ActionResult TestAction()
        //{
        //    var tour = _tourService.LoadBasicsWith(1);
        //    var matches = tour.Matches.ToList();
        //    var matchlist = _matchService.GenerateMatchlist(matches);
        //    var scorelist = _matchService.GenerateScorelist(matches, true);
        //    var headers = new List<string>(){"Дата", "Дома", "В гостях", "Счет"};
        //    var table = new MatchTableViewModel(headers, matchlist, scorelist);
        //    return View(table);
        //}
    }
}