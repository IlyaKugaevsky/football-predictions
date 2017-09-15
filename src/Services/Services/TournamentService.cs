using Persistence.DAL;

namespace Services.Services
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