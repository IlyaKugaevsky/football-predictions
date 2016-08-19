using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Predictions.ViewModels;
using Predictions.DAL;
using Predictions.Models;
using System.Net;

namespace Predictions.Controllers
{
    public class MatchesController : Controller
    {
        // GET: Matches
        public ActionResult Index()
        {
            using (var context = new PredictionsContext())
            {
                //refactor later
                //in ideal world - only services
                var teamlist = context.Teams.ToList();
                var matchlist = context.Matches.ToList();

                //foreach(var m in matchlist)
                //{
                //    if (m.HomeTeam == null)
                //    {
                //        m.HomeTeam = new Team()
                //        {
                //            Title = "Noname"
                //        };
                //    }
                //    if (m.AwayTeam == null)
                //    {
                //        m.AwayTeam = new Team()
                //        {
                //            Title = "Noname"
                //        };
                //    }
                //}

                MatchesViewModel viewModel = new MatchesViewModel()
                {
                    Teamlist = teamlist,
                    Matchlist = matchlist,
                    TourId = 1
                };

                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult Index(MatchesViewModel model)
        {
            //[Bind(Include = "MatchId,AwayTeam,HomeTeam")]
            if (ModelState.IsValid)
            {
                using (var context = new PredictionsContext())
                {

                    //NEED SERVICES!!!!
                    //find by Id, add 
                    Team homeTeam = context.Teams.Find(model.HomeTeamId);
                    Team awayTeam = context.Teams.Find(model.AwayTeamId);

                    var tour = context.Tours.Find(model.TourId);

                    Match match = new Match()
                    {
                        HomeTeam = homeTeam,
                        AwayTeam = awayTeam,
                        Date = model.Date,
                        Tour = tour
                    };

                    context.Matches.Add(match);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }

        public ActionResult Delete(int? id)
        {
            if(id == null) 
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var context = new PredictionsContext())
            {
                Match match = context.Matches.Find(id);
                context.Matches.Remove(match);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
        }


    }
}