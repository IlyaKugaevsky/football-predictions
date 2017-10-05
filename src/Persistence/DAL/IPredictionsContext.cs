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
        DbSet<Tournament> Tournaments { get; set; }
        DbSet<Expert> Experts { get; set; }
        DbSet<Team> Teams { get; set; }
        DbSet<Match> Matches { get; set; }
        DbSet<Prediction> Predictions { get; set; }
        DbSet<OldTour> OldTours { get; set; }
        DbSet<Tour> Tours { get; set; }

        int SaveChanges();
    }
}
