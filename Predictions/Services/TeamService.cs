using Predictions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;


namespace Predictions.Services
{
    public class TeamService
    {
        private readonly PredictionsContext _context;

        public TeamService(PredictionsContext context)
        {
            _context = context;
        }

        public List<SelectListItem> GenerateSelectList()
        {
            var teamlist = new List<SelectListItem>();
            _context.Teams.ToList().
                ForEach(e => teamlist.Add(
                    new SelectListItem() { Text = e.Title, Value = e.TeamId.ToString() }));
            return teamlist;
        }

        public List<string> GenerateOrderedTeamTitlelist (int tourId)
        {
            var teamlist = new List<string>();

            var tour = _context.Tours
                    .Include(t => t.Matches
                        .Select(m => m.HomeTeam))
                    .Include(t => t.Matches
                        .Select(m => m.AwayTeam))
                    .ToList()
                    .Single(t => t.TourId == tourId);

            var matches = tour.Matches;

            matches.ForEach(m =>
            {
                teamlist.Add(m.HomeTeam.Title);
                teamlist.Add(m.AwayTeam.Title);
            });

            return teamlist;
        }
    }
}