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
                viewModel.SelectedExpertId = 1;
                viewModel.Tourlist = context.Tours.ToList();

                var tour = context.Tours
                    .Include(t => t.Matches
                        .Select(m => m.HomeTeam))
                    .Include(t => t.Matches
                        .Select(m => m.AwayTeam))
                    .Include(t => t.Matches
                        .Select(m => m.Predictions
                            /*.SingleOrDefault(p => p.ExpertId == viewModel.SelectedExpertId)*/))
                    .SingleOrDefault(t => t.TourId == viewModel.SelectedTourId);

                if (tour == null)
                {
                    return HttpNotFound();
                }

                viewModel.Tour = tour;

                return View(viewModel);
            }

        }
    }
}