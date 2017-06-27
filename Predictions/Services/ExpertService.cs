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

        public List<Expert> GetExperts()
        {
            return _context.Experts.ToList();
        }
    }
}