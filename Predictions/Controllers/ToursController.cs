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

namespace Predictions.Controllers
{
    public class ToursController : Controller
    {
        public ActionResult Index()
        {
            using (var context = new PredictionsContext())
            {
                var tours = context.Tours
                    .Include(t => t.Matches
                        .Select(m => m.HomeTeam))
                    .Include(t => t.Matches
                        .Select(m => m.AwayTeam))
                    .ToList();
                return View(tours);
            }
        }

        public ActionResult EditTour(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var context = new PredictionsContext())
            {
                Tour tour = context.Tours
                   .Include(t => t.Matches
                       .Select(m => m.HomeTeam))
                   .Include(t => t.Matches
                       .Select(m => m.AwayTeam))
                   .SingleOrDefault(t => t.TourId == id);

                if (tour == null)
                {
                    return HttpNotFound();
                }
                var teamlist = context.Teams.ToList();

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
                    //NEED SERVICES!!!!
                    //find by Id, add 
                    var homeTeam = context.Teams.Find(viewModel.SelectedHomeTeamId);
                    var awayTeam = context.Teams.Find(viewModel.SelectedAwayTeamId);

                    Match match = new Match()
                    {
                        HomeTeam = homeTeam,
                        AwayTeam = awayTeam,
                        Date = viewModel.InputDate,
                        TourId = viewModel.Tour.TourId //TO FIX 
                    };
                    context.Matches.Add(match);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(viewModel);
        } 

        //a lot of work here!
        //validation
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
                    .SingleOrDefault(t => t.TourId == id);
                if (tour == null)
                {
                    return HttpNotFound();
                }

                //if predicion already exist? TODO

                //var matchlist = tour.Matches.ToList(); //really need?
                var expertlist = context.Experts.ToList();

                AddPredictionsViewModel viewModel = new AddPredictionsViewModel()
                {
                    Tour = tour,
                    //Matchlist = matchlist,
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
                    for(var i = 0; i <= viewModel.Tour.Matches.Count - 1; i++)
                    {
                        predictionlist.Add
                        (
                            new Prediction()
                            {
                                Value = viewModel.InputPredictionValuelist.ElementAt(i),
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

                //if score already exist? TODO

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

    }
}