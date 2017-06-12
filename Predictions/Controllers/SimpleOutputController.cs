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
        //public ActionResult Index()
        //{
        //    var prediction = "2:1";
        //    var score = "2:1";

        //    return Content(
        //        "прогноз: " + prediction + "<br />"
        //        + "результат: " + score + "<br />"
        //        + "<br />"
        //        + "голы домашней команды в прогнозе: " + PredictionEvaluator.GetHomeGoals(prediction).ToString() + "<br />"
        //        + "голы гостевой команды в прогнозе: " + PredictionEvaluator.GetAwayGoals(prediction).ToString() + "<br />"
        //        + "<br />"
        //        + "голы домашней команды в результате: " + PredictionEvaluator.GetHomeGoals(score).ToString() + "<br />"
        //        + "голы гостевой команды в результате: " + PredictionEvaluator.GetAwayGoals(score).ToString() + "<br />"
        //        + "<br />"
        //        + "разница мячей в прогнозе: " + PredictionEvaluator.GetDifference(prediction).ToString() + "<br />"
        //        + "разница мячей в результате: " + PredictionEvaluator.GetDifference(score).ToString() + "<br />"
        //        + "<br />"
        //        + "угадан ли счет? - " + PredictionEvaluator.PredictScore(prediction, score).ToString() + "<br />"
        //        + "угадана ли разница? - " + PredictionEvaluator.PredictDifference(prediction, score).ToString() + "<br />"
        //        + "угадан ли исход? - " + PredictionEvaluator.PredictOutcome(prediction, score).ToString() + "<br />"
        //        + "<br />"
        //        + "итого: " + PredictionEvaluator.GetPredictionResults(prediction, score).ToString() + "<br />"
        //        );
        //}
    }
}