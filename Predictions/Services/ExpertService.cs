using System;
using Predictions.DAL;
using Predictions.Models;
using Predictions.ViewModels.Basis;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Predictions.Services
{
    public class ExpertService
    {
        private readonly PredictionsContext _context;

        public ExpertService(PredictionsContext context)
        {
            _context = context;
        }
 
        public List<SelectListItem> GenerateSelectList()
        {
            var expertlist = new List<SelectListItem>();
            _context.Experts.ToList().
                ForEach(e => expertlist.Add(
                    new SelectListItem() { Text = e.Nickname, Value = e.ExpertId.ToString() }));
            return expertlist;
        }
    }
}