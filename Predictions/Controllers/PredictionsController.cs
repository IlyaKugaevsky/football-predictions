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
        private readonly PredictionsContext _context;
        private readonly ExpertService _expertService;
        private readonly TourService _tourService;

        public PredictionsController()
        {
            _context = new PredictionsContext();
            _expertService = new ExpertService(_context);
            _tourService = new TourService(_context);
        }

        public ActionResult PredictionsDisplay()
        {
            using (var context = new PredictionsContext())
            {
                var viewModel = new PredictionsDisplayViewModel()
                {
                    Expertlist = _expertService.GenerateSelectList(),
                    Tourlist = _tourService.GenerateSelectList()
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
                    var tour = _tourService.LoadBasicsWith(viewModel.SelectedTourId, t => t.Matches.Select(m => m.Predictions));

                    //mb not so effective

                    //var tour = context.Tours
                    //    .Include(t => t.Matches
                    //        .Select(m => m.HomeTeam))
                    //    .Include(t => t.Matches
                    //        .Select(m => m.AwayTeam))
                    //    .Include(t => t.Matches
                    //        .Select(m => m.Predictions))
                    //    .Single(t => t.TourId == viewModel.SelectedTourId);


                    if (tour == null) return HttpNotFound();

                    viewModel.Matchlist = new List<MatchInfo>();

                    for (var i = 0; i <= tour.Matches.Count() - 1; i++)
                    {
                        viewModel.Matchlist.Add
                        (
                            new MatchInfo(tour.Matches[i].Date, tour.Matches[i].HomeTeam.Title, tour.Matches[i].AwayTeam.Title)
                        );

                        var filteredPredictions = tour.Matches[i].Predictions
                            .Where(p => p.ExpertId == viewModel.SelectedExpertId).ToList();

                        //just change to smth like predictionlist 

                        //if (filteredPredictions.Count == 0 || filteredPredictions == null) viewModel.Matchlist[i].PredictionValue = "N/A";
                        //else viewModel.Matchlist[i].PredictionValue = filteredPredictions.Count == 1 ? filteredPredictions[0].Value : "несколько";
                    }
                    return View(viewModel);
                };
            }
            //WTF!!!
            else return HttpNotFound();
        }
    }
}