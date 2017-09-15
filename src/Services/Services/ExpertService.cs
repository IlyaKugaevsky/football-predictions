using System.Collections.Generic;
using System.Linq;
using Core.Models;
using Persistence.DAL;
using Persistence.DAL.EntityFrameworkExtensions;

namespace Services.Services
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
            return _context.GetExperts().ToList();
        }
    }
}