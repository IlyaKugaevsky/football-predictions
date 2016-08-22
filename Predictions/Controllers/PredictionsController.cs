using Predictions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Predictions.ViewModels;

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

        public ActionResult Index(PredictionsDisplayViewModel viewModel)
        {
            using (var context = new PredictionsContext())
            {
                var expertlist = context.Experts.ToList();
                var tourlist = context.Tours.ToList();

                return View(viewModel);
            }

        }
    }
}