using Predictions.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Predictions.DAL
{
    public class PredictionsContext : DbContext
    {
        public PredictionsContext() : base("PredictionsContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Expert> Experts { get; set; }
        public DbSet<Team> Teams { get; set;  }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Prediction> Predictions { get; set; }
        public DbSet<OldTour> OldTours { get; set; }

        public DbSet<Tour> NewTours { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}