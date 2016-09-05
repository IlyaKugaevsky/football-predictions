using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Predictions.Models;
using Predictions.DAL;
using Predictions.ViewModels;
using Predictions.ViewModels.Basis;

namespace Predictions.Services
{
    public class MatchService
    {
        private readonly PredictionsContext _context;

        public MatchService(PredictionsContext context)
        {
            _context = context;
        }

        public List<MatchInfo> GenerateMatchlist(List<Match> matches)
        {
            var matchlist = new List<MatchInfo>();
            for (var i = 0; i < matches.Count; i++)
            {
                matchlist.Add(new MatchInfo(matches[i].Date, matches[i].HomeTeam.Title, matches[i].AwayTeam.Title));
            }
            return matchlist;
        }

        public Match CreateMatch(DateTime date, int homeId, int awayId, int tourId)
        {
            var match = new Match()
            {
                Date = date,
                HomeTeam = _context.Teams.Find(homeId),
                AwayTeam = _context.Teams.Find(awayId),
                TourId = tourId 
            };
            return match;
        }

        public void AddMatch(Match match)
        {
            _context.Matches.Add(match);
            _context.SaveChanges();
        } 

        public void AddScores(List<Match> matches, List<FootballScore> scorelist)
        {
            for (var i = 0; i < matches.Count(); i++)
            {
                matches[i].Score = scorelist[i].Value;
            }
            _context.SaveChanges();
        }
    }
}