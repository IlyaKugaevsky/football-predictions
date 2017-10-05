using Persistence.DAL;

namespace Services.Services
{
    public class TournamentService
    {
        private readonly IPredictionsContext _context;

        public TournamentService(IPredictionsContext context)
        {
            _context = context;
        }
    }
}