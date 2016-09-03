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
        private readonly ExpertService _expertService;
        private readonly TourService _tourService;
        private readonly PredictionService _predictionService;
        private readonly MatchService _matchService;
        private readonly TeamService _teamService;



        public ToursController()
        {
            _expertService = new ExpertService();
            _tourService = new TourService();
            _predictionService = new PredictionService();
            _matchService = new MatchService();
            _teamService = new TeamService();
        }

        public ActionResult Index()
        {
            using (var context = new PredictionsContext())
            {
                return View(_tourService.LoadBasicsWith(context));
            }
        }

        public ActionResult EditTour(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            using (var context = new PredictionsContext())
            {
                int realId = id.GetValueOrDefault();
                Tour tour = _tourService.LoadBasicsWith(realId, context);

                if (tour == null) return HttpNotFound();
                var teamlist = _teamService.GenerateSelectList(context);

                EditTourViewModel viewModel = new EditTourViewModel()
                {
                    Teamlist = teamlist,
                    Tour = tour
                };
                return View(viewModel);
            }
        }

        //TODO: real tour update
        [HttpPost]
        public ActionResult EditTour(EditTourViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                using (var context = new PredictionsContext())
                {
                    var match = _matchService.CreateMatch(
                        viewModel.SelectedHomeTeamId,
                        viewModel.SelectedAwayTeamId,
                        viewModel.Tour.TourId,
                        viewModel.InputDate,
                        context);

                    _matchService.AddMatch(match, context);
                    return RedirectToAction("Index");
                }
            }
            return View(viewModel);
        } 

        //a lot of work here!
        public ActionResult AddPredictions(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var context = new PredictionsContext())
            {
                var tour = context.Tours
                    .Include(t => t.Matches
                        .Select(m => m.HomeTeam))
                    .Include(t => t.Matches
                        .Select(m => m.AwayTeam))
                    .Include(t => t.Matches
                        .Select(m => m.Predictions))
                    .Single(t => t.TourId == id);

                if (tour == null)
                {
                    return HttpNotFound();
                }

                //if predicion already exist? TODO

                //var expertlist = context.Experts.ToList();

                var expertlist = _expertService.GenerateSelectList(context);

                AddPredictionsViewModel viewModel = new AddPredictionsViewModel()
                {
                    Tour = tour,
                    Expertlist = expertlist
                };
                return View(viewModel);
            };
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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var context = new PredictionsContext())
            {
                var tour = context.Tours
                    .Include(t => t.Matches
                        .Select(m => m.HomeTeam))
                    .Include(t => t.Matches
                        .Select(m => m.AwayTeam))
                    .SingleOrDefault(t => t.TourId == id);

                if (tour == null)
                {
                    return HttpNotFound();
                }

                //if the score already exists? TODO

                var matchlist = new List<MatchInfo>();

                for(var i = 0; i < tour.Matches.Count; i++)
                {
                    matchlist.Add(
                        new MatchInfo()
                        {
                            Date = tour.Matches[i].Date,
                            HomeTeamTitle = tour.Matches[i].HomeTeam.Title,
                            AwayTeamTitle = tour.Matches[i].AwayTeam.Title
                        });

                }
                AddScoresViewModel viewModel = new AddScoresViewModel()
                {
                    CurrentTourId = tour.TourId,
                    Matchlist = matchlist
                };

                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult AddScores(AddScoresViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                using (var context = new PredictionsContext())
                {
                    //quite ugly

                    var scorelist = new List<String>();

                    for (var i = 0; i < viewModel.Matchlist.Count(); i++)
                    {
                        scorelist.Add(viewModel.InputScorelist[i]);
                    }

                    var tour = context.Tours
                        .Include(t => t.Matches)
                        .SingleOrDefault(t => t.TourId == viewModel.CurrentTourId);

                    for (var i = 0; i < tour.Matches.Count(); i++)
                    {
                        tour.Matches[i].Score = viewModel.InputScorelist[i];
                    }
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return Content(ModelState.Values.ElementAt(0).Errors.ElementAt(0).Exception.ToString()); //change later
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
                var service = new PredictionService();
                service.SubmitTourPredictions(correctId, context);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
        }

    }
}