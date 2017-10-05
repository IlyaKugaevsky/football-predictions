using System.Collections.Generic;
using System.Linq;
using Core.Models;
using Persistence.DAL;
using Persistence.DAL.EntityFrameworkExtensions;
using Persistence.DAL.FetchStrategies;
using Persistence.DAL.FetchStrategies.TournamentsFetchStrategies;
using Persistence.DAL.FetchStrategies.ToursFetchStrategies;
using Services.Helpers;

namespace Services.Services
{
    public class TeamService
    {
        private readonly IPredictionsContext _context;

        public TeamService(IPredictionsContext context)
        {
            _context = context;
        }

        public List<Team> GetLastTournamentTeams()
        {
            var fetchStrategies = new IFetchStrategy<Tournament>[]
            {
                new FetchToursWithMatchesWithHomeTeam(),
                new FetchToursWithMatchesWithAwayTeam()
            };
            var firstTour = Queryable.First<Tour>(_context.GetLastTournamentTours(fetchStrategies));

            if (firstTour.Matches.IsNullOrEmpty()) return Enumerable.ToList<Team>(_context.Teams);

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
                new FetchMatchesWithHomeTeam(),
                new FetchMatchesWithAwayTeam()
            };

            var tour = Queryable.Single<Tour>(_context.GetTours(fetchStrategies), t => t.TourId == tourId);
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