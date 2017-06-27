﻿using Predictions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Predictions.Models;
using Predictions.DAL.FetchStrategies;
using Predictions.DAL.FetchStrategies.TourFetchStrategies;
using Predictions.DAL.FetchStrategies.TournamentFetchStrategies;
using Predictions.DAL.EntityFrameworkExtensions;

namespace Predictions.Services
{
    public class TeamService
    {
        private readonly PredictionsContext _context;

        public TeamService(PredictionsContext context)
        {
            _context = context;
        }

        public List<Team> GetLastTournamentTeams()
        {
            var fetchStrategies = new IFetchStrategy<Tournament>[]
            {
                new ToursWithMatchesWithHomeTeam(),
                new ToursWithMatchesWithAwayTeam()
            };
            var firstTour = _context.GetLastTournamentTours(fetchStrategies).First();

            if (firstTour.Matches.IsNullOrEmpty()) return _context.Teams.ToList();

            var teams = new List<Team>();
            firstTour.Matches.ForEach(m =>
            {
                teams.Add(m.HomeTeam);
                teams.Add(m.AwayTeam);
            });
            return teams;
        }

        public List<string> GenerateOrderedTeamTitlelist(int tourId)
        {
            var fetchStrategies = new IFetchStrategy<Tour>[]
            {
                new MatchesWithHomeTeam(),
                new MatchesWithAwayTeam()
            };

            var tour = _context.GetTours(fetchStrategies).Single(t => t.TourId == tourId);
            var matches = tour.Matches;

            var teamlist = new List<string>();
            matches.ForEach(m =>
            {
                teamlist.Add(m.HomeTeam.Title);
                teamlist.Add(m.AwayTeam.Title);
            });

            return teamlist;
        }
    }
}