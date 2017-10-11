using System.Linq;
using Core.Models;
using Core.QueryExtensions;
using Persistence.DAL.FetchStrategies;

namespace Persistence.DAL.EntityFrameworkExtensions
{
    public static class ToursExtensions
    {
        public static IQueryable<Tour> GetTours(this IPredictionsContext context, IFetchStrategy<Tour>[] fetchStrategies)
        {
            var appliedStrategies = fetchStrategies.Select(fs => fs.Apply()).ToArray();
            return context.Tours.IncludeMultiple<Tour>(appliedStrategies);
        }

        public static IQueryable<Tour> GetLastTournamentTours(this IPredictionsContext context, IFetchStrategy<Tournament>[] fetchStrategies)
        {
            var appliedStrategies = fetchStrategies.Select(fs => fs.Apply()).ToArray();
            var tournaments = context.Tournaments.IncludeMultiple<Tournament>(appliedStrategies);
            var lastTournament = tournaments.LastAdded();
            return lastTournament.NewTours.AsQueryable();
        }

        public static IQueryable<Tour> GetToursByTournamentId(this IPredictionsContext context, int tournamentId, IFetchStrategy<Tournament>[] fetchStrategies)
        {
            var appliedStrategies = fetchStrategies.Select(fs => fs.Apply()).ToArray();
            var tournaments = context.Tournaments.IncludeMultiple<Tournament>(appliedStrategies);
            var tournament = tournaments.TournamentById(tournamentId);
            return tournament.NewTours.AsQueryable();
        }
    }
}