using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Predictions.Helpers;

namespace Predictions.Controllers
{
    public class SimpleOutputController : Controller
    {
        // GET: SimpleOutput
        public ActionResult Index()
        {
            var prediction = "1:0";
            var score = "3:1";

            return Content("прогноз: " + prediction + "<br />"
                + "голы домашней команды в прогнозе: " + PredictionEvaluator.GetHomeGoals(prediction).ToString() + "<br />"
                + "голы гостевой команды в прогнозе: " + PredictionEvaluator.GetAwayGoals(prediction).ToString() + "<br />"
                + "<br />"
                + "результат: " + score + "<br />"
                + "голы домашней команды в результате: " + PredictionEvaluator.GetHomeGoals(score).ToString() + "<br />"
                + "голы гостевой команды в результате: " + PredictionEvaluator.GetAwayGoals(score).ToString() + "<br />"
                + "<br />");
        }
    }
}