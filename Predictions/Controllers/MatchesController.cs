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
            using (var context = new PredictionsContext())
            {
                //refactor later
                //in ideal world - only services
                var teamlist = context.Teams.ToList();
                var matchlist = context.Matches.ToList();

                MatchesViewModel model = new MatchesViewModel(teamlist, matchlist);

                return View(model);
            }
        }


    }
}