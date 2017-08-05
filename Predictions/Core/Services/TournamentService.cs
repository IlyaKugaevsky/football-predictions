using Predictions.DAL;

namespace Predictions.Core.Services
{
    public class TournamentService
    {
        private readonly PredictionsContext _context;

        public TournamentService(PredictionsContext context)
        {
            _context = context;
        }
    }
}