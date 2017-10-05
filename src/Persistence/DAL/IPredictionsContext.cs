using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;

namespace Persistence.DAL
{
    public interface IPredictionsContext
    {
        IDbSet<Tournament> Tournaments { get; set; }
        IDbSet<Expert> Experts { get; set; }
        IDbSet<Team> Teams { get; set; }
        IDbSet<Match> Matches { get; set; }
        IDbSet<Prediction> Predictions { get; set; }
        IDbSet<OldTour> OldTours { get; set; }
        IDbSet<Tour> Tours { get; set; }

        int SaveChanges();
    }
}
