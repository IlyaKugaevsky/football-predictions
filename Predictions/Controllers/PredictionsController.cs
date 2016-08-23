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
            using (var context = new PredictionsContext())
            {
                //var expert = context.Experts.Find

                //test!
                //viewModel.SelectedExpertId = 1;
                viewModel.Tourlist = context.Tours.ToList();
                viewModel.Expertlist = context.Experts.ToList();
                ///

                var tour = context.Tours
                    .Include(t => t.Matches
                        .Select(m => m.HomeTeam))
                    .Include(t => t.Matches
                        .Select(m => m.AwayTeam))
                    .Include(t => t.Matches
                        .Select(m => m.Predictions
                        /*.Where(p => p.ExpertId == viewModel.SelectedExpertId)*/))
                    .SingleOrDefault(t => t.TourId == viewModel.SelectedTourId);

                if (tour == null)
                {
                    return HttpNotFound();
                }



                viewModel.Matchlist = new List<MatchInfo>();

                for (var i = 0; i <= tour.Matches.Count() - 1; i++)
                {
                    viewModel.Matchlist.Add(new MatchInfo());
                    //viewModel.TourInfo.Matchlist[i].Date = tour.Matches[i].Date;
                    viewModel.Matchlist[i].HomeTeamTitle = tour.Matches[i].HomeTeam.Title;
                    viewModel.Matchlist[i].AwayTeamTitle = tour.Matches[i].AwayTeam.Title;
                    viewModel.Matchlist[i].Date = tour.Matches[i].Date;

                    var test = tour.Matches[i].Predictions
                        .Where(p => p.ExpertId == viewModel.SelectedExpertId).ToList();

                    //if (tour.Matches[i].Predictions.Count == 0 || tour.Matches[i].Predictions == null) viewModel.Matchlist[i].PredictionValue = "N/A";
                    //else viewModel.Matchlist[i].PredictionValue = tour.Matches[i].Predictions.Count == 1 ? tour.Matches[i].Predictions[0].Value : "несколько";

                    if (test.Count == 0 || test == null) viewModel.Matchlist[i].PredictionValue = "N/A";
                    else viewModel.Matchlist[i].PredictionValue = test.Count == 1 ? test[0].Value : "несколько";


                }

                return View(viewModel);
            }

        }
    }
}