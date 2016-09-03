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
using Predictions.Services;

namespace Predictions.Controllers
{
    public class PredictionsController : Controller
    {
        private readonly ExpertService _expertService;
        private readonly TourService _tourService;

        public PredictionsController()
        {
            _expertService = new ExpertService();
            _tourService = new TourService();
        }

        public ActionResult PredictionsDisplay()
        {
            using (var context = new PredictionsContext())
            {
                var viewModel = new PredictionsDisplayViewModel()
                {
                    Expertlist = _expertService.GenerateSelectList(context),
                    Tourlist = _tourService.GenerateSelectList(context)
                };
                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult PredictionsDisplay(PredictionsDisplayViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                using (var context = new PredictionsContext())
                {

                    //var tour = _tourService.LoadMatchInfoWithPredictions(viewModel.SelectedTourId, context);

                    var tour = _tourService.EagerLoadById
                        (
                            viewModel.SelectedTourId,
                            context,
                            t => t.Matches.Select(m => m.HomeTeam),
                            t => t.Matches 
                        );

                    //mb not so effective

                    //var tour = context.Tours
                    //    .Include(t => t.Matches
                    //        .Select(m => m.HomeTeam))
                    //    .Include(t => t.Matches
                    //        .Select(m => m.AwayTeam))
                    //    .Include(t => t.Matches
                    //        .Select(m => m.Predictions))
                    //    .Single(t => t.TourId == viewModel.SelectedTourId);


                    if (tour == null)
                    {
                        return HttpNotFound();
                    }

                    viewModel.Matchlist = new List<MatchInfo>();

                    for (var i = 0; i <= tour.Matches.Count() - 1; i++)
                    {
                        viewModel.Matchlist.Add
                        (
                            new MatchInfo()
                            {
                                Date = tour.Matches[i].Date,
                                HomeTeamTitle = tour.Matches[i].HomeTeam.Title,
                                AwayTeamTitle = tour.Matches[i].AwayTeam.Title
                            }
                        );

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