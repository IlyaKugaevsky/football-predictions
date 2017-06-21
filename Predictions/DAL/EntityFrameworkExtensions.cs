using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Predictions.DAL.FetchStrategies;
using Predictions.Models;

namespace Predictions.DAL
{
    public static class EntityFrameworkExtensions
    {
        public static IQueryable<Tour> ToursWithMatchesWithTeams(this PredictionsContext context)
        {
            return context.Tours
                .Include(t => t.Matches
                    .Select(m => m.HomeTeam))
                .Include(t => t.Matches
                    .Select(m => m.AwayTeam));
        }

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

        public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query,
            params Expression<Func<T, object>>[] includes) 
            where T : class
        {
            if (includes != null)
            {
                query = includes.Aggregate(query,
                    (current, include) => current.Include(include));
            }
            return query;
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
            {
                return true;
            }
            /* If this is a list, use the Count property for efficiency. 
             * The Count property is O(1) while IEnumerable.Count() is O(N). */
            var collection = enumerable as ICollection<T>;
            if (collection != null)
            {
                return collection.Count < 1;
            }
            return !enumerable.Any();
        }
    }
}