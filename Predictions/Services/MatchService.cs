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

        //to think; scores, predictions? mb options
        public List<MatchInfo> GenerateMatchlist(List<Match> matches)
        {
            return matches.Select(m => new MatchInfo(m.Date, m.HomeTeam.Title, m.AwayTeam.Title)).ToList();
            //var matchlist = new List<MatchInfo>();
            //for (var i = 0; i < matches.Count; i++)
            //{
            //    matchlist.Add(new MatchInfo(matches[i].Date, matches[i].HomeTeam.Title, matches[i].AwayTeam.Title, null, matches[i].Score));
            //}
            //return matchlist;
        }

        public List<FootballScore> GenerateScoreList(List<Match> matches)
        {
            return  matches.Select(m => new FootballScore { Value =  m.Score}).ToList();
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