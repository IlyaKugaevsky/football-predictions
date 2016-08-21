using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Predictions.Controllers
{
    public class PredictionsController : Controller
    {
        // GET: Predictions
        public ActionResult Index()
        {
            return View();
        }
    }
}