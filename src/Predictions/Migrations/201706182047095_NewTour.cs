namespace Predictions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewTour : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NewTour",
                c => new
                    {
                        NewTourId = c.Int(nullable: false, identity: true),
                        TournamentId = c.Int(nullable: false),
                        TourNumber = c.Int(nullable: false),
                        IsClosed = c.Boolean(nullable: false),
                        StartDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        EndDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.NewTourId)
                .ForeignKey("dbo.Tournament", t => t.TournamentId, cascadeDelete: false)
                .Index(t => t.TournamentId);
            
            AddColumn("dbo.Match", "NewTour_NewTourId", c => c.Int());
            CreateIndex("dbo.Match", "NewTour_NewTourId");
            AddForeignKey("dbo.Match", "NewTour_NewTourId", "dbo.NewTour", "NewTourId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NewTour", "TournamentId", "dbo.Tournament");
            DropForeignKey("dbo.Match", "NewTour_NewTourId", "dbo.NewTour");
            DropIndex("dbo.NewTour", new[] { "TournamentId" });
            DropIndex("dbo.Match", new[] { "NewTour_NewTourId" });
            DropColumn("dbo.Match", "NewTour_NewTourId");
            DropTable("dbo.NewTour");
        }
    }
}
