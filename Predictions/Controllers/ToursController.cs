using Predictions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Predictions.Controllers
{
    public class ToursController : Controller
    {
        // GET: Tour
        public ActionResult Index()
        {
            using (var context = new PredictionsContext())
            {
                var tours = context.Tours.ToList();
                return View(tours);
            }

        }
    }
}