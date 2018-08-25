using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Core.Models;

namespace Persistence.DAL
{
    public class PredictionsContext : DbContext, IPredictionsContext
    {
        public PredictionsContext() : base("PredictionsContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<Tournament> Tournaments { get; set; }
        public virtual DbSet<Expert> Experts { get; set; }
        public virtual DbSet<Team> Teams { get; set;  }
        public virtual DbSet<Match> Matches { get; set; }
        public virtual DbSet<Prediction> Predictions { get; set; }
        public virtual DbSet<OldTour> OldTours { get; set; }

        public virtual DbSet<HeadToHeadTournament> HeadToHeadTournaments { get; set; }
        public virtual DbSet<HeadToHeadTour> HeadToHeadTours { get; set; }
        public virtual DbSet<HeadToHeadMatch> HeadToHeadMatches { get; set; }

        public virtual DbSet<Tour> Tours { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}