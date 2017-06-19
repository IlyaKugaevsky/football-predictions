using Predictions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Predictions.Models;
using Predictions.ViewModels;
using Predictions.Services;
using System.Net;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using Predictions.ViewModels.Basis;
using Predictions.Helpers;

namespace Predictions.Controllers
{
    public class CurrentTournamentToursController : Controller
    {
        private readonly PredictionsContext _context;
        private readonly ExpertService _expertService;
        private readonly TourService _tourService;
        private readonly PredictionService _predictionService;
        private readonly MatchService _matchService;
        private readonly TeamService _teamService;
        private readonly FileService _fileService;
        private readonly TournamentService _tournamentService;
        //private readonly int _currentTournamentId;

        public CurrentTournamentToursController()
        {
            _context = new PredictionsContext();
            _expertService = new ExpertService(_context);
            _tourService = new TourService(_context);
            _predictionService = new PredictionService(_context);
            _matchService = new MatchService(_context);
            _teamService = new TeamService(_context);
            _tournamentService = new TournamentService(_context);

            //constructor with params?
            _fileService = new FileService();

            //_currentTournamentId = _tournamentService.GetCurrentTournamentId();
        }

        public ActionResult Index()
        {
            //var newTour1 = new NewTour(3, 1, false);
            //var newTour2 = new NewTour(3, 2, false);
            //_context.NewTours.Add(newTour1);
            //_context.NewTours.Add(newTour2);
            //_context.SaveChanges();


            //var newTours = _context.NewTours.ToList();
            //var tours = _context.Tours.Include(t => t.Matches).ToList();

            //var tournament = _context.Tournaments.Find(1);
            //tournament.NewTours = newTours;


            //newTours.ForEach(nt => nt.Matches = tours.Find(t => t.TourId == nt.NewTourId).Matches);
            //_context.NewTours.AddRange(newTours);
            //_context.SaveChanges();

            //var tournament = _context.Tournaments.Include(t => t.NewTours).First();

            return View(_tourService.GetLastTournamentSchedule());
        }

        //404 after deleting
        //model or Dto?
        public ActionResult EditTour(int tourId)
        {
            //optimization: tour with matches
            var tourDto = _tourService.GetTourDto(tourId);
            var teams = _teamService.GetLastTournamentTeams();
            var matches = _matchService.GetLastTournamentMatchesByTourId(tourId);
            var scorelist = _matchService.GenerateScorelist(tourId);

            return View(new EditTourViewModel(teams, matches, scorelist, tourDto));
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "SaveTourSettings")]
        public ActionResult SaveTourSettings(EditTourViewModel viewModel)
        {
            //id nullcheck
            if (ModelState.IsValid)
            {
                _tourService.UpdateTour(viewModel.NewTourDto);
                return RedirectToAction("Index");
            }
            return EditTour(viewModel.NewTourDto.TourId);

        }

        //bind include
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "AddMatch")]
        public ActionResult AddMatch(EditTourViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var match = new Match(
                    viewModel.InputDate,
                    viewModel.SelectedHomeTeamId,
                    viewModel.SelectedAwayTeamId,
                    viewModel.NewTourDto.TourId);

                _matchService.AddMatch(match);
                return RedirectToAction("EditTour", new {tourId = viewModel.NewTourDto.TourId });
            }
            return View(viewModel);
        }

        //IParsingResult, error messages
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "AddMatches")]
        public ActionResult AddMatches(EditTourViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                //already in viewModel?
                var possibleTeams = _teamService.GetLastTournamentTeams();

                var inputMatchesInfo = viewModel.SubmitTextArea.InputText;
                var parsingResult = _fileService.ParseTourSchedule(inputMatchesInfo);

                var matches = _matchService.CreateMatches(parsingResult, possibleTeams, viewModel.SubmitTextArea.TourId);

                _matchService.AddMatches(matches);

                //return View("~/Views/CurrentTournamentTours/EditTour.cshtml", viewModel);
                return EditTour(viewModel.SubmitTextArea.TourId);
            }
            //fix
            return EditTour(viewModel.SubmitTextArea.TourId);
        }


        public ActionResult EditPredictions(int tourId, int? expertId, bool addPredictionSuccess = false) 
        {
            var expertlist = _expertService.GenerateSelectList();
            var tourInfo = _tourService.GetTourDto(tourId);

            var headers = new List<string>() { "Дата", "Дома", "В гостях", "Прогноз" };
            var matchlist = _matchService.GetLastTournamentMatchesByTourId(tourId).Select(m => m.GetDto()).ToList();
            var scorelist = _predictionService.GeneratePredictionlist(tourId, expertId, true);

            var matchTable = new MatchTableViewModel(headers, matchlist, scorelist);
            var viewModel = new EditPredictionsViewModel(expertlist, tourInfo, matchTable);
            
            viewModel.SelectedExpertId = expertId ?? 0;
            viewModel.SubmitTextArea.TourId = viewModel.NewTourDto.TourId;
            viewModel.AddPredictionsSuccess = addPredictionSuccess;

            return View(viewModel);
        }

        //bind
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "ShowPredictions")]
        public ActionResult ShowPredictions(EditPredictionsViewModel viewModel)
        {
            return RedirectToAction("EditPredictions", new { tourId = viewModel.NewTourDto.TourId, expertId = viewModel.SelectedExpertId });
        }

        //bind
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "EditPredictions")]
        public ActionResult EditPredictions(EditPredictionsViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _predictionService.AddExpertPredictions(viewModel.SelectedExpertId, viewModel.NewTourDto.TourId, viewModel.MatchTable.Scorelist);
                return RedirectToAction("EditPredictions", new { tourId = viewModel.NewTourDto.TourId, expertId = viewModel.SelectedExpertId });
            }
            return View(viewModel);

        }

        //bind
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "AddPredictions")]
        public ActionResult AddPredictions(EditPredictionsViewModel viewModel)
        {
            var teamlist = _teamService.GenerateOrderedTeamTitlelist(viewModel.SubmitTextArea.TourId);
            var scorelist = _fileService.ParseExpertPredictions(viewModel.SubmitTextArea.InputText, teamlist);
            if (!scorelist.IsNullOrEmpty()) _predictionService.AddExpertPredictions(viewModel.SelectedExpertId, viewModel.SubmitTextArea.TourId, scorelist);

            return RedirectToAction("EditPredictions", new { tourId = viewModel.SubmitTextArea.TourId, expertId = viewModel.SelectedExpertId, addPredictionSuccess = !scorelist.IsNullOrEmpty()
        });
        }


        public ActionResult AddScores(int tourId)
        {
            var matchlist = _matchService.GetLastTournamentMatchesByTourId(tourId).Select(m => m.GetDto()).ToList();
            var scorelist = _matchService.GenerateScorelist(tourId, true);
            if (matchlist == null || scorelist == null) return HttpNotFound();
            var headers = new List<string>() { "Дата", "Дома", "В гостях", "Счет" };
            var matchTable = new MatchTableViewModel(headers, matchlist, scorelist);

            return View(new AddScoresViewModel(tourId, matchTable));
        }

        [HttpPost]
        public ActionResult AddScores([Bind(Include = "MatchTable, CurrentTourId")] AddScoresViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _matchService.AddScores(_tourService.GetMatchesByTour(viewModel.CurrentTourId), viewModel.MatchTable.Scorelist);
                return RedirectToAction("Index");
            }
            return AddScores(viewModel.CurrentTourId); //not sure
        }

        public ActionResult Preresults(int tourId)
        {
            var preresults = _tourService.GenerateTourPreresultlist(tourId);
            var enableSubmit = _tourService.AllResultsReady(tourId);
            var viewModel = new PreresultsViewModel(preresults, _tourService.MatchesCount(tourId), tourId, enableSubmit);
            return View(viewModel);
        }

        public ActionResult SubmitTourPredictions(int tourId)
        {
            _predictionService.SubmitTourPredictions(tourId);
            return RedirectToAction("Index");
        }

        public ActionResult RestartTour(int tourId)
        {
            _predictionService.RestartTour(tourId);
            return RedirectToAction("Index");
        }

        //404 error
        public ActionResult DeleteMatch(int id)
        {
            //so~so, mb better
            var tourId = _matchService.GetTourId(id);
            if (tourId == null) return HttpNotFound(); //this also checks id
            _matchService.DeleteMatch(id);
            return RedirectToAction("EditTour", new { id = tourId });
        }

        //terrible, fix as fast as possible
        public ActionResult DeleteConfirmation(int id)
        {
            var match = _context.Matches.Find(id);

            ViewBag.number = match.Predictions.IsNullOrEmpty() ? 0 : match.Predictions.Count();
            ViewBag.id = id;
            return View();
        }
    }
}