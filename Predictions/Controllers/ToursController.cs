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

        public ActionResult EditTour(int? tourId)
        {
            Tour tour = _tourService.LoadBasicsWith(tourId);
            if (tour == null) return HttpNotFound();
            var teamlist = _teamService.GenerateSelectList();
            return View(new EditTourViewModel(teamlist, tour));
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
                    viewModel.Tour.TourId);
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
            var scorelist = _predictionService.GeneratePredictionlist(tourId, expertId, false);

            var matchTable = new MatchTableViewModel(headers, matchlist, scorelist);
            var viewModel = new AddPredictionsViewModel(expertlist, tourInfo, matchTable);
            return View(viewModel);
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "ShowPredictions")]
        public ActionResult ShowPredictions(AddPredictionsViewModel viewModel)
        {
            return RedirectToAction("AddPredictions", new { tourId = viewModel.TourInfo.TourId, expertId = viewModel.SelectedExpertId });
        }

       
        //public ActionResult AddPredictions([Bind(Include = "TourInfo, SelectedExpertId, EditPredictionsValuelist")] AddPredictionsViewModel viewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //var matches = _tourService.GetMatchesByTour(viewModel.TourInfo.TourId);
        //        //_predictionService.AddExpertPredictions(viewModel.SelectedExpertId, matches, viewModel.MatchTable.Scorelist);
        //        return RedirectToAction("Index");

        //    }
        //    return Content(ModelState.Values.ElementAt(0).Errors.ElementAt(0).Exception.ToString()); //change later
        //}

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
                //really works? mb inputScorelist < matchlist?
                _matchService.AddScores(_tourService.GetMatchesByTour(viewModel.CurrentTourId), viewModel.MatchTable.Scorelist);
                return RedirectToAction("Index");
            }
            return AddScores(viewModel.CurrentTourId); //not sure
        }

        public ActionResult SubmitTourPredictions(int? tourId)
        {
            if (tourId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var context = new PredictionsContext())
            {
                //mb better?
                int correctId = tourId ?? default(int);

                //private, at the top of the class
                _predictionService.SubmitTourPredictions(correctId);
                //to service
                context.SaveChanges();
                return RedirectToAction("Index");
            }
        }

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