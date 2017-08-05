using System.Collections.Generic;
using System.Linq;
using Predictions.DAL;
using Predictions.Core.Models;

namespace Predictions.Core.Services
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