using Predictions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}