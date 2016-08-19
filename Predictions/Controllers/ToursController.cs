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
                //inicialization

                //for (int i = 1; i <= 8; i++)
                //{
                //    TourId tour = new TourId()
                //    {
                //        TourId = i,
                //        StartDate = new DateTime()
                //    };
                //    context.Tours.Add(tour);
                //}
                //context.SaveChanges();

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
                var matchlist = tour.Matchlist;

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
                        //Tour = model.Tour.TourId //TO FIX 
                    };



                    //context.Tours.Add(match);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        } 
    }
}