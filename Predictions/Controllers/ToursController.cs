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
        // GET: TourId
        public ActionResult Index()
        {
            using (var context = new PredictionsContext())
            {
                var tours = context.Tours
                    .Include(t => t.Matchlist
                        .Select(m => m.HomeTeam))
                    .Include(t => t.Matchlist
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
                Tour tour = context.Tours.Find(id);
                if (tour == null)
                {
                    return HttpNotFound();
                }

                var teamlist = context.Teams.ToList();
                var matchlist = tour.Matchlist.ToList();

                EditTourViewModel viewModel = new EditTourViewModel()
                {
                    Teamlist = teamlist,
                    Matchlist = matchlist,
                    Tour = tour
                };

                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult EditTour(EditTourViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var context = new PredictionsContext())
                {
                    //NEED SERVICES!!!!
                    //find by Id, add 
                    Team homeTeam = context.Teams.Find(model.HomeTeamId);
                    Team awayTeam = context.Teams.Find(model.AwayTeamId);

                    Match match = new Match()
                    {
                        HomeTeam = homeTeam,
                        AwayTeam = awayTeam,
                        Date = model.Date,
                        TourId = model.Tour.TourId //TO FIX 
                    };

                    context.Matches.Add(match);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        } 

        public ActionResult AddPrediction(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var context = new PredictionsContext())
            {
                Tour tour = context.Tours
                    .Include(t => t.Matchlist
                        .Select(m => m.HomeTeam))
                    .Include(t => t.Matchlist
                        .Select(m => m.AwayTeam))
                    .SingleOrDefault(t => t.TourId == id);
                if (tour == null)
                {
                    return HttpNotFound();
                }

                var matchlist = tour.Matchlist.ToList(); //really need?
                var expertlist = context.Experts.ToList();

                AddPredictionViewModel viewModel = new AddPredictionViewModel()
                {
                    Tour = tour,
                    Matchlist = matchlist,
                    Expertlist = expertlist
                };
                return View(viewModel);
            };
        } 

        [HttpPost]
        public ActionResult AddPrediction (AddPredictionViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var context = new PredictionsContext())
                {
                    var predictionlist = new List<Prediction>();
                    for(var i = 0; i <= model.Matchlist.Count - 1; i++)
                    {
                        predictionlist.Add
                        (
                            new Prediction()
                            {
                                Value = model.PredictionValuelist.ElementAt(i),
                                MatchId = model.Matchlist.ElementAt(i).MatchId,
                                ExpertId = model.SelectedExpertId
                            }
                        );
                    }
                    predictionlist.ForEach(n => context.Predictions.Add(n));
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return Content(ModelState.Values.ElementAt(0).Errors.ElementAt(0).Exception.ToString());
        }
    }
}