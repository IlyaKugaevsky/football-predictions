using Predictions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Predictions.Models;
using System.Net;

namespace Predictions.Controllers
{
    public class ToursController : Controller
    {
        // GET: Tour
        public ActionResult Index()
        {
            using (var context = new PredictionsContext())
            {
                //inicialization

                //for(int i = 1; i <= 8; i++)
                //{
                //    Tour tour = new Tour()
                //    {
                //        TourId = i,
                //        StartDate = new DateTime()
                //    };
                //    context.Tours.Add(tour);
                //}
                //context.SaveChanges();

                var tours = context.Tours.ToList();
                return View(tours);
            }

        }

        public ActionResult EditTour(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var context = new PredictionsContext())
            {
                Tour tour = context.Tours.Find(id);
                if (tour == null)
                {
                    return HttpNotFound();
                }
                return View(tour);
            }
        }
    }
}