//using Predictions.DAL;

using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Core.Helpers;
using Core.Models;
using Core.Models.Dtos;
using Core.QueryExtensions;
using Persistence.DAL;
using Persistence.DAL.EntityFrameworkExtensions;
using Services.Services;
using Web.Helpers;
using Web.ViewModels;
using Web.AutoMapper.Extensions;
using Web.AutoMapper.Profiles;

//using Predictions.DAL.EntityFrameworkExtensions;

namespace Web.Controllers
{
    public class CurrentTournamentToursController : Controller
    {
        private readonly IPredictionsContext _context;
        private readonly ExpertService _expertService;
        private readonly TourService _tourService;
        private readonly PredictionService _predictionService;
        private readonly MatchService _matchService;
        private readonly TeamService _teamService;
        private readonly FileService _fileService;
        private readonly TournamentService _tournamentService;
        //private readonly IMapper _mapper;

        public CurrentTournamentToursController(IPredictionsContext context, IMapper mapper)
        {
            _expertService = new ExpertService(context);
            _tourService = new TourService(context);
            _predictionService = new PredictionService(context);
            _matchService = new MatchService(context, mapper);
            _teamService = new TeamService(context);
            _tournamentService = new TournamentService(context);

            _context = context;

            // _mapper = mapper;

            _fileService = new FileService();
        }

        public ActionResult Index()
        {
            //_context.Experts.Add(new Expert() {Nickname = "Jonny Vodka" });
            //_context.Teams.AddOrUpdate(t => t.Title, new Team() { Title = "Россия" });
            //_context.Teams.AddOrUpdate(t => t.Title, new Team() { Title = "Сауд. Аравия" });
            //_context.Teams.AddOrUpdate(t => t.Title, new Team() { Title = "Египет" });
            //_context.Teams.AddOrUpdate(t => t.Title, new Team() { Title = "Уругвай" });
            //_context.Teams.AddOrUpdate(t => t.Title, new Team() { Title = "Марокко" });
            //_context.Teams.AddOrUpdate(t => t.Title, new Team() { Title = "Иран" });
            //_context.Teams.AddOrUpdate(t => t.Title, new Team() { Title = "Португалия" });
            //_context.Teams.AddOrUpdate(t => t.Title, new Team() { Title = "Испания" });
            //_context.Teams.AddOrUpdate(t => t.Title, new Team() { Title = "Франция" });
            //_context.Teams.AddOrUpdate(t => t.Title, new Team() { Title = "Австралия" });
            //_context.Teams.AddOrUpdate(t => t.Title, new Team() { Title = "Перу" });
            //_context.Teams.AddOrUpdate(t => t.Title, new Team() { Title = "Дания" });
            //_context.Teams.AddOrUpdate(t => t.Title, new Team() { Title = "Аргентина" });
            //_context.Teams.AddOrUpdate(t => t.Title, new Team() { Title = "Исландия" });
            //_context.Teams.AddOrUpdate(t => t.Title, new Team() { Title = "Хорватия" });
            //_context.Teams.AddOrUpdate(t => t.Title, new Team() { Title = "Нигерия" });
            //_context.Teams.AddOrUpdate(t => t.Title, new Team() { Title = "Коста-Рика" });
            //_context.Teams.AddOrUpdate(t => t.Title, new Team() { Title = "Сербия" });
            //_context.Teams.AddOrUpdate(t => t.Title, new Team() { Title = "Бразилия" });
            //_context.Teams.AddOrUpdate(t => t.Title, new Team() { Title = "Швейцария" });
            //_context.Teams.AddOrUpdate(t => t.Title, new Team() { Title = "Германия" });
            //_context.Teams.AddOrUpdate(t => t.Title, new Team() { Title = "Мексика" });
            //_context.Teams.AddOrUpdate(t => t.Title, new Team() { Title = "Швеция" });
            //_context.Teams.AddOrUpdate(t => t.Title, new Team() { Title = "Южная Корея" });
            //_context.Teams.AddOrUpdate(t => t.Title, new Team() { Title = "Бельгия" });
            //_context.Teams.AddOrUpdate(t => t.Title, new Team() { Title = "Панама" });
            //_context.Teams.AddOrUpdate(t => t.Title, new Team() { Title = "Тунис" });
            //_context.Teams.AddOrUpdate(t => t.Title, new Team() { Title = "Англия" });
            //_context.Teams.AddOrUpdate(t => t.Title, new Team() { Title = "Колумбия" });
            //_context.Teams.AddOrUpdate(t => t.Title, new Team() { Title = "Япония" });
            //_context.Teams.AddOrUpdate(t => t.Title, new Team() { Title = "Польша" });
            //_context.Teams.AddOrUpdate(t => t.Title, new Team() { Title = "Сенегал" });

            //var tours = new List<Tour>()
            //{
            //    new Tour()
            //    {
            //        IsClosed = false,
            //        TourNumber = 1,
            //        StartDate = new DateTime(2018, 6, 14, 18, 0, 0),
            //        EndDate = new DateTime(2018, 6, 19, 20, 0, 0)
            //    },
            //    new Tour()
            //    {
            //        IsClosed = false,
            //        TourNumber = 2,
            //        StartDate = new DateTime(2018, 6, 19, 21, 0, 0),
            //        EndDate = new DateTime(2018, 6, 24, 23, 0, 0)
            //    },
            //    new Tour()
            //    {
            //        IsClosed = false,
            //        TourNumber = 3,
            //        StartDate = new DateTime(2018, 6, 25, 17, 0, 0),
            //        EndDate = new DateTime(2018, 6, 28, 19, 0, 0)
            //    },



            //};

            //_context.Tournaments.Add(new Tournament()
            //{
            //    Title = "Чемпионат мира 2018",
            //    StartDate = new DateTime(2018, 6, 14, 18, 0, 0),
            //    EndDate = new DateTime(2018, 7, 15, 18, 0, 0),
            //    NewTours = tours
            //});

            //foreach (var expert in _context.Experts)
            //{
            //    expert.Sum = 0;
            //    expert.Scores = 0;
            //    expert.Differences = 0;
            //    expert.Outcomes = 0;
            //}

            //_context.Experts.Add(new Expert() {Nickname = "Авоська"});
            //_context.Experts.Add(new Expert() { Nickname = "Глас народа" });

            //_context.SaveChanges();

            return View(_tourService.GetLastTournamentSchedule());
        }

        //model or Dto?
        public ActionResult EditTour(int tourId)
        {
            //optimization: tour with matches
            var tourDto = _tourService.GetTourDto(tourId);
            var teams = _teamService.GetLastTournamentTeams();
            //var matches = _matchService.GetLastTournamentMatchesByTourId(tourId);
            var matches = _matchService.GetTourSchedule(tourId);
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

            if (inputMatchesInfo.IsNullOrEmpty())
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
            //var matches = _matchService.GetLastTournamentMatchesByTourId(tourId).ToList();
            var matches = _matchService.GetTourSchedule(tourId);
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
                viewModel.MatchTable.Scorelist.Select(s => s.Score).ToList());
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
            if (!scorelist.IsNullOrEmpty())
                _predictionService.AddExpertPredictions(viewModel.SelectedExpertId, viewModel.SubmitTextArea.TourId, scorelist.Select(s => s.Score).ToList());

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
            //var matches = _matchService.GetLastTournamentMatchesByTourId(tourId).Select(m => m.GetDto()).ToList();
            var tourNumber = _tourService.GetTourDto(tourId).TourNumber;
            var matches = _matchService.GetTourSchedule(tourId).Select(m => m.GetDto()).ToList();

            var scorelist = _matchService.GenerateScorelist(tourId, true);
            return View(new AddScoresViewModel(tourId, tourNumber, matches, scorelist));
        }

        [HttpPost]
        public ActionResult AddScores([Bind(Include = "MatchTable, CurrentTourId")] AddScoresViewModel viewModel)
        {
            if (!ModelState.IsValid) return AddScores(viewModel.CurrentTourId); //not sure

            _matchService.AddScores(_matchService.GetMatchesByTourId(viewModel.CurrentTourId), viewModel.MatchTable.Scorelist);
            return RedirectToAction("Index");
        }

        public ActionResult Preresults(int tourId)
        {
            var preresults = _tourService.GenerateTourPreresultlist(tourId);
            //var enableSubmit = _matchService.AllMatchScoresPopulated(tourId);
            var enableSubmit = _matchService.GetMatchesByTourId(tourId).AllScoresNotNullOrEmpty();
            var viewModel = new PreresultsViewModel(preresults, _matchService.MatchesCount(tourId), tourId, enableSubmit);
            return View(viewModel);
        }

        public ActionResult SubmitTourPredictions(int tourId)
        {
            //var meanScores = _expertService.GenerateMeanPredictions(tourId);
            //var randomScores = _expertService.GenerateRandomizerPredictions(meanScores.Count);
            //var slavaScores = _expertService.GenerateVenceslavaPredictions(meanScores.Count);

            //_predictionService.AddExpertPredictions(16, tourId, slavaScores.ToList());
            //_predictionService.AddExpertPredictions(24, tourId, randomScores.ToList());
            //_predictionService.AddExpertPredictions(25, tourId, meanScores.ToList());

            _predictionService.AddBotPredictions(tourId);

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
            //mb better
            var tourId = _matchService.GetTourId(id);
            _matchService.DeleteMatch(id);
            return RedirectToAction("EditTour", new { tourId = tourId });
        }
    }
}