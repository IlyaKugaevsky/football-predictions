using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Predictions.Models;
using Predictions.DAL;
using Predictions.ViewModels;
using Predictions.ViewModels.Basis;
using System.Data.Entity;
using Predictions.DAL.FetchStrategies;
using Predictions.DAL.FetchStrategies.TourFetchStrategies;
using Predictions.DAL.FetchStrategies.TournamentFetchStrategies;
using Predictions.Models.Dtos;

namespace Predictions.Services
{
    public class MatchService
    {
        private readonly PredictionsContext _context;

        public MatchService(PredictionsContext context)
        {
            _context = context;
        }

        public List<Match> GetTourSchedule(int tourId)
        {
            var fetchStrategies = new IFetchStrategy<Tour>[]
            {
                new MatchesWithHomeTeam(),
                new MatchesWithAwayTeam()
            };

            var tours = _context.GetTours(fetchStrategies);
            var tour = tours.Single(t => t.TourId == tourId);

            return tour.Matches.ToList();
        }

        public List<Match> GetLastTournamentMatchesByTourId(int tourId)
        {
            var fetchStrategies = new IFetchStrategy<Tournament>[]
            {
                new ToursWithMatchesWithHomeTeam(),
                new ToursWithMatchesWithAwayTeam()
            };

            var tours = _context.GetLastTournamentTours(fetchStrategies);
            var tour = tours.Single(t => t.TourId == tourId);

            return tour.Matches.ToList();
        }

        public List<MatchDto> GenerateMatchlist(int tournamentId, int tourId)
        {
            var fetchStrategies = new IFetchStrategy<Tournament>[]
            {
                new ToursWithMatchesWithHomeTeam(),
                new ToursWithMatchesWithAwayTeam()
            };

            var tours = _context.GetLastTournamentTours(fetchStrategies);
            var tour = tours.Single(t => t.TourId == tourId);

            return tour.Matches.Select(m => m.GetDto()).ToList();
        }

        public List<FootballScore> GenerateScorelist(int tourId, bool editable = false, string emptyDisplay = "-")
        {

            //var tour = _context.Tours
            //        .Include(t => t.Matches
            //            .Select(m => m.HomeTeam))
            //        .Include(t => t.Matches
            //            .Select(m => m.AwayTeam))
            //        .Single(t => t.TourId == tourId);

            //var tour = _context.ToursWithMatchesWithTeams().Single(t => t.TourId == tourId);

            var fetchStrategies = new IFetchStrategy<Tour>[]
            {
                new MatchesWithHomeTeam(),
                new MatchesWithAwayTeam()
            };

            var tour = _context.GetTours(fetchStrategies).Single(t => t.TourId == tourId);

            //var tour = _context.g


            //return tour.Matches.Select(m => new FootballScore
            //{
            //    Value = (String.IsNullOrEmpty(m.Score) && editable == false) ? emptyDisplay : m.Score,
            //    Editable = editable 
            //}).ToList();

            return tour.Matches.Select(m => m.GetFootballScore(editable, emptyDisplay)).ToList();

        }

        public List<FootballScore> GenerateScorelist(List<Match> matches, bool editable = false, string emptyDisplay = "-")
        {
            return matches.Select(m => m.GetFootballScore(editable, emptyDisplay)).ToList();
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


        public int? GetTourId(int? matchId)
        {
            if (matchId == null) return null;
            var match = _context.Matches.Find(matchId);
            if (match == null) return null;
            return match.TourId;
        }

        //always null-check before execute this
        public void DeleteMatch(int? id)
        {
            var match = _context.Matches.Find(id);
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