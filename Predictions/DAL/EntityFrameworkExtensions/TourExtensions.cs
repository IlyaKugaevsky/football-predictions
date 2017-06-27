using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Predictions.DAL.FetchStrategies;
using Predictions.Models;

namespace Predictions.DAL.EntityFrameworkExtensions
{
    public static class TourExtensions
    {
        public static IQueryable<Tour> GetTours(this PredictionsContext context, IFetchStrategy<Tour>[] fetchStrategies)
        {
            var appliedStrategies = fetchStrategies.Select(fs => fs.Apply()).ToArray();
            return context.Tours.IncludeMultiple<Tour>(appliedStrategies);
        }

        public static IQueryable<Tour> GetLastTournamentTours(this PredictionsContext context, IFetchStrategy<Tournament>[] fetchStrategies)
        {
            var appliedStrategies = fetchStrategies.Select(fs => fs.Apply()).ToArray();
            var tournaments = context.Tournaments.IncludeMultiple<Tournament>(appliedStrategies);
            var lastTournament = tournaments
                    .OrderByDescending(t => t.TournamentId)
                    .First();

            return lastTournament.NewTours.AsQueryable();
        }

        public static IQueryable<Tour> GetToursByTournamentId(this PredictionsContext context, int tournamentId, IFetchStrategy<Tournament>[] fetchStrategies)
        {
            var appliedStrategies = fetchStrategies.Select(fs => fs.Apply()).ToArray();
            var tournaments = context.Tournaments.IncludeMultiple<Tournament>(appliedStrategies);
            var tournament = tournaments
                .Single(t => t.TournamentId == tournamentId);

            return tournament.NewTours.AsQueryable();
        }
    }
}