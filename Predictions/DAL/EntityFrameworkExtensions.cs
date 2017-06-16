using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public static IQueryable<Tour> GetLastTournamentTours(this PredictionsContext context, IFetchStrategy<Tournament>[] fetchStrategies)
        {
            var appliedStrategies = fetchStrategies.Select(fs => fs.Apply()).ToArray();
            var tournaments = context.Tournaments.IncludeMultiple<Tournament>(appliedStrategies);
            var lastTournament = tournaments
                    .OrderByDescending(t => t.TournamentId)
                    .First();

            return lastTournament.Tours.AsQueryable();
        }

        public static IQueryable<Tour> LastTournamentToursWithMatchesTeams(this PredictionsContext context)
        {
            return context.Tournaments
                    .Include(trnm => trnm.Tours
                        .Select(tr => tr.Matches
                            .Select(m => m.HomeTeam)))
                    .Include(trnm => trnm.Tours
                        .Select(tr => tr.Matches
                            .Select(m => m.AwayTeam)))
                    .OrderByDescending(t => t.TournamentId)
                    .First()
                    .Tours
                    .AsQueryable();
        }
        public static IQueryable<Tour> LastTournamentToursWithMatchesPredictionsExperts(this PredictionsContext context)
        {
            return context.Tournaments
                    .Include(trnm => trnm.Tours
                        .Select(tr => tr.Matches
                            .Select(m => m.Predictions
                                .Select(p => p.Expert))))
                    .OrderByDescending(t => t.TournamentId)
                    .First()
                    .Tours
                    .AsQueryable();
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