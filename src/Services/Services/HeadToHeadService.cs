using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using Persistence.DAL;
using Persistence.DAL.EntityFrameworkExtensions;
using Persistence.DAL.FetchStrategies;
using Persistence.DAL.FetchStrategies.TournamentsFetchStrategies;
using Services.Helpers;

namespace Services.Services
{
    public class HeadToHeadService
    {
        private readonly IPredictionsContext _context;

        public HeadToHeadService(IPredictionsContext context)
        {
            _context = context;
        }

        public IDictionary<int, int> GetExpertTourSums(IEnumerable<Match> matches)
        {
            var predictionSumByExpert = new Dictionary<int, int>();

            foreach (var match in matches)
            {
                foreach (var prediction in match.Predictions.OrEmptyIfNull())
                {
                    if (!prediction.IsClosed)
                    {
                        predictionSumByExpert.Clear();
                        throw new ArgumentException("All predictions must be closed.");
                    }

                    var expert = prediction.Expert;

                    if (predictionSumByExpert.ContainsKey(expert.ExpertId))
                    {
                        predictionSumByExpert[expert.ExpertId] = predictionSumByExpert[expert.ExpertId] + prediction.Sum;
                    }
                    else
                    {
                        predictionSumByExpert[expert.ExpertId] = prediction.Sum;
                    }
                }
            }

            return predictionSumByExpert;
        }

        public void EvaluateTour(int headToHeadTourId)
        {
            var headToHeadTour = _context.HeadToHeadTours.Include(t => t.Matches).Single(t => t.HeadToHeadTourId == headToHeadTourId);
            var tourId = headToHeadTour.ParentTourId;
            var headToHeadMatches = headToHeadTour.Matches;

            ////////////////////////////////////////////////////////////////////////////////////////////////

            var fetchStrategies = new IFetchStrategy<Tournament>[]
            {
                new FetchToursWithMatchesWithPredictionsWithExperts()
            };
            var tours = _context.GetLastTournamentTours(fetchStrategies);

            var tour = tours.Single(t => t.TourId == tourId);
            if (!tour.IsClosed) throw new InvalidOperationException("Tour must be closed.");

            var matches = tour.Matches;
            var predictionSumByExpert = GetExpertTourSums(matches);

            ////////////////////////////////////////////////////////////////////////////////////////////////

            foreach (var match in headToHeadMatches)
            {
                match.HomeGoals = (byte)predictionSumByExpert[match.HomeExpertId];
                match.AwayGoals = (byte)predictionSumByExpert[match.AwayExpertId];
            }

            _context.SaveChanges();
        }

        public IDictionary<string, LeagueTableLine> EvaluateTable(int headToheadTournamentId, int toursNumber)
        {
            var table = new Dictionary<string, LeagueTableLine>();

            var headToHEadTournament = _context.HeadToHeadTournaments
                .Include(t => t.Tours.Select(tour => tour.Matches.Select(m => m.HomeExpert)))
                .Include(t => t.Tours.Select(tour => tour.Matches.Select(m => m.AwayExpert)))
                .AsNoTracking()
                .Single(t => t.HeadToHeadTournamentId == headToheadTournamentId);

            var tours = headToHEadTournament.Tours;

            foreach (var tour in tours.Take(toursNumber))
            {
                foreach (var match in tour.Matches)
                {
                    var homeExpert = match.HomeExpert;
                    var awayExpert = match.AwayExpert;
                    var homeGoals = match.HomeGoals;
                    var awayGoals = match.AwayGoals;

                    if (!table.ContainsKey(homeExpert.Nickname))
                    {
                        table[homeExpert.Nickname] = new LeagueTableLine();
                    }

                    if (!table.ContainsKey(awayExpert.Nickname))
                    {
                        table[awayExpert.Nickname] = new LeagueTableLine();
                    }

                    table[homeExpert.Nickname].ScoredGoals += homeGoals;
                    table[homeExpert.Nickname].ConcededGoals += awayGoals;

                    table[awayExpert.Nickname].ScoredGoals += awayGoals;
                    table[homeExpert.Nickname].ConcededGoals += homeGoals;

                    if (homeGoals > awayGoals)
                    {
                        table[homeExpert.Nickname].Points += 3;
                        table[homeExpert.Nickname].Wins++;
                        table[awayExpert.Nickname].Looses++;

                    }
                    else if (homeGoals < awayGoals)
                    {
                        table[awayExpert.Nickname].Points += 3;
                        table[awayExpert.Nickname].Wins++;
                        table[homeExpert.Nickname].Looses++;
                    }
                    else
                    {
                        table[homeExpert.Nickname].Points += 1;
                        table[awayExpert.Nickname].Points += 1;
                        table[awayExpert.Nickname].Draws++;
                        table[homeExpert.Nickname].Draws++;
                    }
                }
            }

            return table;
        }


        //var tours = _context.HeadToHeadTours.Include(t => t.Matches).ToList();
        //var pairs = expertGenerator.RoundsConstruct(4);

        //    for (var i = 0; i<pairs.Count / 4; i++)
        //{
        //    for (var j = 0; j< 4; j++)
        //    {
        //        var match = new HeadToHeadMatch() { HomeExpertId = pairs[i * 4 + j].Item1.ExpertId, AwayExpertId = pairs[i * 4 + j].Item2.ExpertId };
        //        tours[i].Matches.Add(match);
        //    }
        //    lines.Add($"{tours[i].Matches.Count}");
        //}
    }
}
