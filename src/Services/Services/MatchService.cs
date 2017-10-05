using System.Collections.Generic;
using System.Linq;
using Core.Models;
using Core.Models.Dtos;
using Persistence.DAL;
using Persistence.DAL.EntityFrameworkExtensions;
using Persistence.DAL.FetchStrategies;
using Persistence.DAL.FetchStrategies.TournamentsFetchStrategies;
using Persistence.DAL.FetchStrategies.ToursFetchStrategies;
using Services.Helpers;

namespace Services.Services
{
    public class MatchService
    {
        private readonly IPredictionsContext _context;

        public MatchService(IPredictionsContext context)
        {
            _context = context;
        }

        public int MatchesCount(int tourId)
        {
            var fetchStrategies = new IFetchStrategy<Tour>[] { new FetchMatches() };
            return Queryable.Single<Tour>(_context.GetTours(fetchStrategies), t => t.TourId == tourId).Matches.Count();
        }

        public bool AllResultsAreReady(int tourId)
        {
            var fetchStrategies = new IFetchStrategy<Tour>[] { new FetchMatches() };
            var matches = Queryable.Single<Tour>(_context.GetTours(fetchStrategies), t => t.TourId == tourId).Matches;
            return !matches.Any(m => m.Score.IsNullOrEmpty());
        }

        public List<Match> GetMatchesByTour(int tourId)
        {
            var fetchStrategies = new IFetchStrategy<Tour>[] { new FetchMatches() };
            return Queryable.Single<Tour>(_context.GetTours(fetchStrategies), t => t.TourId == tourId).Matches;
        }


        public List<Match> GetTourSchedule(int tourId)
        {
            var fetchStrategies = new IFetchStrategy<Tour>[]
            {
                new FetchMatchesWithHomeTeam(),
                new FetchMatchesWithAwayTeam()
            };

            var tours = _context.GetTours(fetchStrategies);
            var tour = Queryable.Single<Tour>(tours, t => t.TourId == tourId);

            return tour.Matches.ToList();
        }

        public List<Match> GetLastTournamentMatchesByTourId(int tourId)
        {
            var fetchStrategies = new IFetchStrategy<Tournament>[]
            {
                new FetchToursWithMatchesWithHomeTeam(),
                new FetchToursWithMatchesWithAwayTeam()
            };

            var tours = _context.GetLastTournamentTours(fetchStrategies);
            var tour = Queryable.Single<Tour>(tours, t => t.TourId == tourId);

            return tour.Matches.ToList();
        }

        public List<MatchDto> GenerateMatchlist(int tournamentId, int tourId)
        {
            var fetchStrategies = new IFetchStrategy<Tournament>[]
            {
                new FetchToursWithMatchesWithHomeTeam(),
                new FetchToursWithMatchesWithAwayTeam()
            };
            var tours = _context.GetLastTournamentTours(fetchStrategies);
            var tour = Queryable.Single<Tour>(tours, t => t.TourId == tourId);

            return tour.Matches.Select(m => m.GetDto()).ToList();
        }

        public List<FootballScore> GenerateScorelist(int tourId, bool editable = false, string emptyDisplay = "-")
        {
            var fetchStrategies = new IFetchStrategy<Tour>[]
            {
                new FetchMatchesWithHomeTeam(),
                new FetchMatchesWithAwayTeam()
            };

            var tour = Queryable.Single<Tour>(_context.GetTours(fetchStrategies), t => t.TourId == tourId);
            return tour.Matches.Select(m => m.GetFootballScore(editable, emptyDisplay)).ToList();

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
            if (match == null) throw new KeyNotFoundException("no match with Id = " + matchId.ToString());
            return match.TourId;
        }

        //always null-check before execute this
        public void DeleteMatch(int matchId)
        {
            var match = _context.Matches.Find(matchId);
            if (match == null) throw new KeyNotFoundException("no match with id = " + matchId.ToString());
            _context.Matches.Remove(match);
            _context.SaveChanges();
        }

        public void AddScores(IList<Match> matches, IList<FootballScore> scorelist)
        {
            for (var i = 0; i < matches.Count(); i++)
            {
                matches[i].Score = scorelist[i].Value;
            }
            _context.SaveChanges();
        }
    }
}