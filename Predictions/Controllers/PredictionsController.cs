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
    public class PredictionsController : Controller
    {
        // GET: Predictions
        public ActionResult Index()
        {
            using (var context = new PredictionsContext())
            {
                var expertlist = context.Experts.ToList();
                var tourlist = context.Tours.ToList();

                var viewModel = new PredictionsDisplayViewModel()
                {
                    Expertlist = expertlist,
                    Tourlist = tourlist
                };
                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult Index(PredictionsDisplayViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                using (var context = new PredictionsContext())
                {
                    //mb not so effective
                    var tour = context.Tours
                        .Include(t => t.Matches
                            .Select(m => m.HomeTeam))
                        .Include(t => t.Matches
                            .Select(m => m.AwayTeam))
                        .Include(t => t.Matches
                            .Select(m => m.Predictions))
                        .SingleOrDefault(t => t.TourId == viewModel.SelectedTourId);
                    if (tour == null)
                    {
                        return HttpNotFound();
                    }

                    viewModel.Matchlist = new List<MatchInfo>();
                    for (var i = 0; i <= tour.Matches.Count() - 1; i++)
                    {
                        viewModel.Matchlist.Add(
                            new MatchInfo()
                            {
                                Date = tour.Matches[i].Date,
                                HomeTeamTitle = tour.Matches[i].HomeTeam.Title,
                                AwayTeamTitle = tour.Matches[i].AwayTeam.Title
                            });

                        var filteredPredictions = tour.Matches[i].Predictions
                            .Where(p => p.ExpertId == viewModel.SelectedExpertId).ToList();

                        if (filteredPredictions.Count == 0 || filteredPredictions == null) viewModel.Matchlist[i].PredictionValue = "N/A";
                        else viewModel.Matchlist[i].PredictionValue = filteredPredictions.Count == 1 ? filteredPredictions[0].Value : "несколько";
                    }
                    return View(viewModel);
                };
            }
            //WTF!!!
            else return View(viewModel);
        }
    }
}