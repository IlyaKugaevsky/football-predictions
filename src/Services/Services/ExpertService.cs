using System.Collections.Generic;
using System.Linq;
using Core.Models;
using Persistence.DAL;
using Persistence.DAL.EntityFrameworkExtensions;

namespace Services.Services
{
    public class ExpertService
    {
        private readonly IPredictionsContext _context;

        public ExpertService(IPredictionsContext context)
        {
            _context = context;
        }

        public List<Expert> GetExperts()
        {
            return _context.GetExperts().ToList();
        }
    }
}