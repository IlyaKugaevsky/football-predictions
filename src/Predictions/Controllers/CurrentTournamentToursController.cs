//using Predictions.DAL;
using System.Linq;
using System.Web.Mvc;
using Predictions.ViewModels;
//using Predictions.Core.Models;
//using Predictions.Core.Services;
using Core.Models;
using Core.Models.Dtos;
using Persistence.DAL;
using Persistence.DAL.EntityFrameworkExtensions;
using Predictions.Helpers;
using Services.Services;
using Services.Helpers;

//using Predictions.DAL.EntityFrameworkExtensions;

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
        }

        public ActionResult Index()
        {
            //var tour = new Tour(3, 4);
            //_context.Tours.Add(tour);
            //_context.SaveChanges();

            return View(_tourService.GetLastTournamentSchedule());
        }

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
            //model cannot be invalid (?)
            //tourNumber in TourDto not set
            _tourService.UpdateTour(viewModel.TourDto);
            return RedirectToAction("EditTour", new { tourId = viewModel.TourDto.TourId});
        }

        //bind include
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "AddMatch")]
        public ActionResult AddMatch(EditTourViewModel viewModel)
        {
            if (!ModelState.IsValid) return RedirectToAction("EditTour", new {tourId = viewModel.TourDto.TourId});

            var match = new Match(
                viewModel.InputDate,
                viewModel.SelectedHomeTeamId,
                viewModel.SelectedAwayTeamId,
                viewModel.TourDto.TourId);
            _matchService.AddMatch(match);
            return RedirectToAction("EditTour", new { tourId = viewModel.TourDto.TourId });
        }

        //IParsingResult, error messages
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "AddMatches")]
        public ActionResult AddMatches(EditTourViewModel viewModel)
        {
            var possibleTeams = _teamService.GetLastTournamentTeams();
            var inputMatchesInfo = viewModel.SubmitTextArea.InputText;

            if (GenericsHelper.IsNullOrEmpty(inputMatchesInfo))
                return RedirectToAction("EditTour", new {tourId = viewModel.SubmitTextArea.TourId});

            var parsingResult = _fileService.ParseTourSchedule(inputMatchesInfo);
            var matches = _matchService.CreateMatches(parsingResult, possibleTeams, viewModel.SubmitTextArea.TourId);
            _matchService.AddMatches(matches);
            return RedirectToAction("EditTour", new { tourId = viewModel.SubmitTextArea.TourId});
        }


        public ActionResult EditPredictions(int tourId, int expertId = 1, bool addPredictionSuccess = false) 
        {
            var experts = _expertService.GetExperts();
            var tourDto = _tourService.GetTourDto(tourId);
            var matches = _matchService.GetLastTournamentMatchesByTourId(tourId).ToList();
            var scorelist = _predictionService.GeneratePredictionlist(tourId, expertId, true);
            var viewModel = new EditPredictionsViewModel(matches, experts,  scorelist, tourDto,expertId, addPredictionSuccess);

            return View(viewModel);
        }

        //bind
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "ShowPredictions")]
        public ActionResult ShowPredictions(EditPredictionsViewModel viewModel)
        {
            return RedirectToAction(
                "EditPredictions", 
                new
                {
                    tourId = viewModel.TourDto.TourId,
                    expertId = viewModel.SelectedExpertId
                });
        }

        //bind
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "EditPredictions")]
        public ActionResult EditPredictions(EditPredictionsViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            _predictionService.AddExpertPredictions(
                viewModel.SelectedExpertId, 
                viewModel.TourDto.TourId, 
                viewModel.MatchTable.Scorelist);
            return RedirectToAction(
                "EditPredictions", 
                new
                {
                    tourId = viewModel.TourDto.TourId,
                    expertId = viewModel.SelectedExpertId
                });
        }

        //bind
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "AddPredictions")]
        public ActionResult AddPredictions(EditPredictionsViewModel viewModel)
        {
            var teamlist = _teamService.GenerateOrderedTeamTitlelist(viewModel.SubmitTextArea.TourId);
            var scorelist = _fileService.ParseExpertPredictions(viewModel.SubmitTextArea.InputText, teamlist);
            if (!GenericsHelper.IsNullOrEmpty(scorelist))
                _predictionService.AddExpertPredictions(viewModel.SelectedExpertId, viewModel.SubmitTextArea.TourId, scorelist);

            return RedirectToAction(
                "EditPredictions",
                new
                {
                    tourId = viewModel.SubmitTextArea.TourId,
                    expertId = viewModel.SelectedExpertId,
                    addPredictionSuccess = !GenericsHelper.IsNullOrEmpty(scorelist)
                });
        }


        public ActionResult AddScores(int tourId)
        {
            var matches = _matchService.GetLastTournamentMatchesByTourId(tourId).Select(m => m.GetDto()).ToList();
            var scorelist = _matchService.GenerateScorelist(tourId, true);
            return View(new AddScoresViewModel(tourId, matches, scorelist));
        }

        [HttpPost]
        public ActionResult AddScores([Bind(Include = "MatchTable, CurrentTourId")] AddScoresViewModel viewModel)
        {
            if (!ModelState.IsValid) return AddScores(viewModel.CurrentTourId); //not sure

            _matchService.AddScores(_matchService.GetMatchesByTour(viewModel.CurrentTourId), viewModel.MatchTable.Scorelist);
            return RedirectToAction("Index");
        }

        public ActionResult Preresults(int tourId)
        {
            var preresults = _tourService.GenerateTourPreresultlist(tourId);
            var enableSubmit = _matchService.AllResultsAreReady(tourId);
            var viewModel = new PreresultsViewModel(preresults, _matchService.MatchesCount(tourId), tourId, enableSubmit);
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

        public ActionResult DeleteMatch(int id)
        {
            //so~so, mb better
            var tourId = _matchService.GetTourId(id);
            _matchService.DeleteMatch(id);
            return RedirectToAction("EditTour", new { tourId = tourId });
        }

        //terrible, fix as fast as possible
        public ActionResult DeleteConfirmation(int id)
        {
            var match = _context.Matches.Find(id);

            ViewBag.number = GenericsHelper.IsNullOrEmpty(match.Predictions) ? 0 : match.Predictions.Count();
            ViewBag.id = id;
            return View();
        }
    }
}