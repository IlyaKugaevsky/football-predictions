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
        }

        public ActionResult EditTour(int? id)
        {
            Tour tour = _tourService.LoadBasicsWith(id);
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

        //a lot of work here!
        public ActionResult AddPredictions(int? id)
        {
            var tour = _tourService.LoadBasicsWith(id, t => t.Matches.Select(m => m.Predictions));

            //var tour = context.Tours
            //    .Include(t => t.Matches
            //        .Select(m => m.HomeTeam))
            //    .Include(t => t.Matches
            //        .Select(m => m.AwayTeam))
            //    .Include(t => t.Matches
            //        .Select(m => m.Predictions))
            //    .Single(t => t.TourId == id);

            if (tour == null) return HttpNotFound();

            //if predicion already exist? TODO
            var expertlist = _expertService.GenerateSelectList();

            AddPredictionsViewModel viewModel = new AddPredictionsViewModel()
            {
                Tour = tour,
                Expertlist = expertlist
            };
            return View(viewModel);
        } 

        [HttpPost]
        public ActionResult AddPredictions (AddPredictionsViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                using (var context = new PredictionsContext())
                {
                    var predictionlist = new List<Prediction>();

                    //if valuelist null?
                    for(var i = 0; i <= viewModel.Tour.Matches.Count - 1; i++)
                    {
                        predictionlist.Add
                        (
                            new Prediction()
                            {
                                Value = viewModel.InputData[i].PredictionValue,
                                MatchId = viewModel.Tour.Matches.ElementAt(i).MatchId,
                                ExpertId = viewModel.SelectedExpertId
                            }
                        );
                    }
                    predictionlist.ForEach(n => context.Predictions.Add(n));
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return Content(ModelState.Values.ElementAt(0).Errors.ElementAt(0).Exception.ToString()); //change later
        }

        public ActionResult AddScores(int? id)
        {
            var tour = _tourService.LoadBasicsWith(id);
            if (tour == null) return HttpNotFound();
            var matchlist = _matchService.GenerateMatchlist(tour.Matches);
            return View(new AddScoresViewModel(tour.TourId, matchlist));
        }

        [HttpPost]
        public ActionResult AddScores([Bind(Include = "InputScorelist, CurrentTourId")] AddScoresViewModel viewModel)
        {
            if (ModelState.IsValid)
            {

                //quite ugly

                // var scorelist = new List<String>();

                //really works? mb inputScorelist < matchlist?
                //for (var i = 0; i < viewModel.InputScorelist.Count; i++)
                //{
                //    scorelist.Add(viewModel.InputScorelist[i].Value);
                //}

                //////////////////////////////////////////////////////////////////////////////////////

                //var tour = _tourService.EagerLoad(viewModel.CurrentTourId, t => t.Matches);

                //for (var i = 0; i < tour.Matches.Count(); i++)
                //{
                //    tour.Matches[i].Score = viewModel.InputScorelist[i].Value;
                //}
                //_context.SaveChanges();

                //if the score already exists? TODO

                _matchService.AddScores(_tourService.GetMatchesByTour(viewModel.CurrentTourId), viewModel.InputScorelist);

                return RedirectToAction("Index");
            }
            return AddScores(viewModel.CurrentTourId); //not sure
        }

        public ActionResult SubmitTourPredictions(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var context = new PredictionsContext())
            {
                //mb better?
                int correctId = id ?? default(int);

                //private, at the top of the class
                _predictionService.SubmitTourPredictions(correctId);
                //to service
                context.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult DeleteMatch(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            using (var context = new PredictionsContext())
            {
                //to service
                Match match = context.Matches.Find(id);
                int tourId = match.TourId;
                context.Matches.Remove(match);
                context.SaveChanges();
                return RedirectToAction("EditTour", new { id = tourId});
            }
        }


    }
}