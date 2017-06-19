using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Predictions.ViewModels;
using Predictions.DAL;
using Predictions.Models;
using System.Net;

namespace Predictions.Controllers
{
    public class MatchesController : Controller
    {
        // GET: Matches
        //public ActionResult Index()
        //{
        //    using (var context = new PredictionsContext())
        //    {
        //        //refactor later
        //        //in ideal world - only services
        //        var teamlist = context.Teams.ToList();
        //        var matchlist = context.Matches.ToList();

        //        MatchesViewModel viewModel = new MatchesViewModel()
        //        {
        //            Teamlist = teamlist,
        //            Matchlist = matchlist,
        //        };
        //        return View(viewModel);
        //    }
        //}

        //[HttpPost]
        //public ActionResult Index(MatchesViewModel viewModel)
        //{
        //    //[Bind(Include = "MatchId,AwayTeam,HomeTeam")]
        //    if (ModelState.IsValid)
        //    {
        //        using (var context = new PredictionsContext())
        //        {

        //            //NEED SERVICES!!!!
        //            //find by Id, add 
        //            Team homeTeam = context.Teams.Find(viewModel.SelectedHomeTeamId);
        //            Team awayTeam = context.Teams.Find(viewModel.SelectedAwayTeamId);

        //            Match match = new Match()
        //            {
        //                HomeTeam = homeTeam,
        //                AwayTeam = awayTeam,
        //                Date = viewModel.InputDate,
        //                TourId = viewModel.SelectedTourId
        //            };
        //            context.Matches.Add(match);
        //            context.SaveChanges();
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return View(viewModel);
        //}

        //public ActionResult Delete(int? id)
        //{
        //    if(id == null) 
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    using (var context = new PredictionsContext())
        //    {
        //        Match match = context.Matches.Find(id);
        //        context.Matches.Remove(match);
        //        context.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //}


    }
}