using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Models;
using Persistence.DAL;
using Persistence.DAL.EntityFrameworkExtensions;
using System.Data.Entity;
using Persistence.DAL.FetchStrategies;
using Persistence.DAL.FetchStrategies.TournamentsFetchStrategies;
using Services.Services;

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

            //var HeadToHeadRpl = new HeadToHeadTournament() {ParentTournamentId = 6};

            //_context.HeadToHeadTournaments.Add(HeadToHeadRpl);
            //_context.SaveChanges();

            //var chester = _context.Experts.Find(5);
            //var mary = _context.Experts.Find(22);

            //var cherocky = _context.Experts.Find(1);
            //var andrea = _context.Experts.Find(7);

            //var ibra = _context.Experts.Find(11);
            //var polidevk = _context.Experts.Find(6);

            //var iva = _context.Experts.Find(10);
            //var avos = _context.Experts.Find(24);

            //var expertGenerator = new ScheduleService<Expert>( new List<Expert>() {chester, mary, cherocky, andrea, ibra, polidevk, iva, avos});

            var lines = new List<string>();

            var h2hService = new HeadToHeadService(_context);

            ////h2hService.EvaluateTour(4);
            ////h2hService.EvaluateTour(5);
            h2hService.EvaluateTour(6);

            var table = h2hService.EvaluateTable(1, 6);

            foreach (var expert in table.Keys)
            {
                lines.Add($"{expert} - {table[expert].Wins} {table[expert].Draws} {table[expert].Looses} {table[expert].ScoredGoals}-{table[expert].ConcededGoals} {table[expert].Points} {Environment.NewLine}");
            }


            //var matches = _context.HeadToHeadMatches.Include(m => m.HomeExpert).Include(m => m.AwayExpert).ToList().Skip(20).Take(4);

            //foreach (var match in matches)
            //{
            //    lines.Add($"{match.HomeExpert.Nickname} {match.HomeGoals} - {match.AwayGoals} {match.AwayExpert.Nickname}");
            //}


            var result = string.Join($"|| {Environment.NewLine}", lines);



            return result;
        }
    }
}