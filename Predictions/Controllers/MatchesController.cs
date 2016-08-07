using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Predictions.ViewModels;
using Predictions.DAL;
using Predictions.Models;

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

                MatchesViewModel viewModel = new MatchesViewModel()
                {
                    Teamlist = teamlist,
                    Matchlist = matchlist,
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
                    //find by Id, add by...
                    var test3 = model.Date;
                    var test = model.HomeTeamId;
                    var test2 = model.AwayTeamId;

                    Team homeTeam = context.Teams.Find(model.HomeTeamId);
                    Team awayTeam = context.Teams.Find(model.AwayTeamId);

                    Match match = new Match()
                    {
                        HomeTeam = homeTeam,
                        AwayTeam = awayTeam,
                        Date = model.Date
                    };

                    context.Matches.Add(match);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }

    }
}