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
        public List<SelectListItem> GenerateSelectList(PredictionsContext context)
        {
            var teamlist = new List<SelectListItem>();
            context.Teams.ToList().
                ForEach(e => teamlist.Add(
                    new SelectListItem() { Text = e.Title, Value = e.TeamId.ToString() }));
            return teamlist;
        }
    }
}