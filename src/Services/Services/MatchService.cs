using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AutoMapper;
using Core.Helpers;
using Core.Models;
using Core.Models.Dtos;
using Core.QueryExtensions;
using Persistence.DAL;
using Persistence.DAL.EntityFrameworkExtensions;
using Persistence.DAL.FetchStrategies;
using Persistence.DAL.FetchStrategies.TournamentsFetchStrategies;
using Persistence.DAL.FetchStrategies.ToursFetchStrategies;

namespace Services.Services
{
    public class MatchService
    {
        private readonly IPredictionsContext _context;
        private readonly IMapper _mapper;

        public MatchService(IPredictionsContext context, IMapper mapper = null)
        {
            _context = context;
            _mapper = mapper;
        }

        public IList<Match> GetMatchesByTourId(int tourId)
        {
            var fetchStrategies = new IFetchStrategy<Tour>[] { new FetchMatches() };
            return _context.GetTours(fetchStrategies).TourById(tourId).Matches;
        }

        public IReadOnlyCollection<Match> GetTourSchedule(int tourId)
        {
            var fetchStrategies = new IFetchStrategy<Tour>[]
            {
                new FetchMatchesWithHomeTeam(),
                new FetchMatchesWithAwayTeam()
            };
            return _context.GetTours(fetchStrategies).TourById(tourId).Matches;
        }

        //rewrite with IQueryable and ISpecification 
        public int MatchesCount(int tourId)
        {
            var fetchStrategies = new IFetchStrategy<Tour>[] { new FetchMatches() };
            return _context.GetTours(fetchStrategies).TourById(tourId).Matches.Count;        
        }

        public bool AllMatchScoresPopulated(int tourId)
        {
            var fetchStrategies = new IFetchStrategy<Tour>[] { new FetchMatches() };
            return _context.GetTours(fetchStrategies).TourById(tourId).Matches.AllScoresNotNullOrEmpty();
        }

        public IReadOnlyCollection<Match> GenerateMatchlist(int tournamentId, int tourId)
        {
            var fetchStrategies = new IFetchStrategy<Tournament>[]
            {
                new FetchToursWithMatchesWithHomeTeam(),
                new FetchToursWithMatchesWithAwayTeam()
            };
            return _context.GetLastTournamentTours(fetchStrategies).TourById(tourId).Matches.ToList();
        }

        public IList<FootballScoreViewModel> GenerateScorelist(int tourId, bool editable = false)
        {
            var fetchStrategies = new IFetchStrategy<Tour>[]
            {
                new FetchMatchesWithHomeTeam(),
                new FetchMatchesWithAwayTeam()
            }; 
            var matches = _context.GetTours(fetchStrategies).TourById(tourId).Matches;
            return matches.GetScores().ToViewModels(editable).ToList();
        }

        public List<Match> CreateMatches(List<ParsingMatchInfo> matches, List<Team> possibleTeams, int tourId)
        {
            return matches.Select(m => new Match(
                m.Date,
                possibleTeams.Single(t => t.Title == m.HomeTeamTitle),
                possibleTeams.Single(t => t.Title == m.AwayTeamTitle),
                tourId)).ToList();
        }

        public void AddMatch(Match match)
        {
            _context.Matches.Add(match);
            _context.SaveChanges();
        }

        public void AddMatches(List<Match> matches)
        {
            _context.Matches.AddRange(matches);
            _context.SaveChanges();
        }

        public int GetTourId(int matchId)
        {
            var match = _context.Matches.Find(matchId);
            if (match == null) throw new KeyNotFoundException($"There is no match with Id = {matchId}");
            return match.TourId;
        }

        public void DeleteMatch(int matchId)
        {
            var match = _context.Matches.Find(matchId);
            if (match == null) throw new KeyNotFoundException($"There is no match with Id = {matchId}");
            _context.Matches.Remove(match);
            _context.SaveChanges();
        }

        //zip?
        public void AddScores(IList<Match> matches, IList<FootballScoreViewModel> scorelist)
        {
            for (var i = 0; i < matches.Count(); i++)
            {
                matches[i].Score = scorelist[i].Score;
            }
            _context.SaveChanges();
        }
    }
}