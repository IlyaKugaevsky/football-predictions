using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Models;
using Persistence.DAL;
using Persistence.DAL.EntityFrameworkExtensions;
using System.Data.Entity;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPredictionsContext _context;

        public HomeController(IPredictionsContext context)
        {
            _context = context;
        }


        //public ActionResult Index()
        //{
        //    return View();
        //}

        public string Index()
        {
            //var urAnz = _context.Matches.Find(446);
            //var spOr = _context.Matches.Find(447);

            //var roAh = _context.Matches.Find(448);
            //var enZe = _context.Matches.Find(449);

            //var arDi = _context.Matches.Find(450);
            //var ruCr = _context.Matches.Find(451);

            //var ufLo = _context.Matches.Find(452);
            //var krCs = _context.Matches.Find(453);

            //urAnz.Score = "0:1";
            //spOr.Score = "1:0";

            //roAh.Score = "1:0";
            //enZe.Score = "0:2";

            //arDi.Score = "0:0";
            //ruCr.Score = "2:1";

            //ufLo.Score = "0:0";
            //krCs.Score = "0:0";

            //_context.SaveChanges();

            //var matches = _context.Matches.Include(m => m.HomeTeam).Include(m => m.AwayTeam).OrderByDescending(m => m.MatchId).ToList().Take(30);

            //var lines = matches.Select(m => $"{m.MatchId} {m.HomeTeam.Title} - {m.AwayTeam.Title} {m.Score} {(m != null ? m.PlayoffWinner : (int?) null)} \n").ToList();

            //var experts = _context.Experts.ToList();

            //experts.ForEach(e =>
            //{
            //    e.Sum = 0;
            //    e.Differences = 0;
            //    e.Outcomes = 0;
            //    e.Scores = 0;
            //});

            //_context.SaveChanges();

            //var tour4 = new Tour(6, 4);
            //var tour5 = new Tour(6, 5);
            //_context.Tours.Add(tour4);
            //_context.Tours.Add(tour5);
            //_context.SaveChanges();

            //var tournaments = _context.Tournaments.ToList();
            //var lines = tournaments.Select(t => $"{t.Title} - {t.TournamentId}").ToList();


            //return string.Join("||", lines);

            //var ars = _context.Predictions.Find(4637);
            //var ural = _context.Predictions.Find(4638);

            //var rub = _context.Predictions.Find(4639);
            //var spar = _context.Predictions.Find(4640);

            //var oren = _context.Predictions.Find(4641);
            //var ufa = _context.Predictions.Find(4642);

            //var loco = _context.Predictions.Find(4643);
            //var eni = _context.Predictions.Find(4644);

            //var toDelete = new List<Prediction>() { ars, ural, rub, spar, oren, ufa, loco, eni };

            //_context.Predictions.RemoveRange(toDelete);
            //_context.SaveChanges();



            return "done";
        }
    }
}