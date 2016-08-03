using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Predictions.ViewModels;
using Predictions.DAL;

namespace Predictions.Controllers
{
    public class MatchesController : Controller
    {
        // GET: Matches
        public ActionResult Index()
        {
            // NEED SOME REFACTORING!!!
            // Repository pattern, services, etc

            PredictionsContext context = new PredictionsContext();

            var teamlist = context.Teams.ToList();
            var matchlist = context.Matches.ToList();

            var model = new MatchesViewModel
            {
                Teamlist = teamlist,
                Matchlist = matchlist
            };

            return View(model);
        }
    }
}