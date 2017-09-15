namespace Predictions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTournament : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tournament",
                c => new
                    {
                        TournamentId = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        EndDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.TournamentId);
            
            AddColumn("dbo.Tour", "Tournament_TournamentId", c => c.Int());
            CreateIndex("dbo.Tour", "Tournament_TournamentId");
            AddForeignKey("dbo.Tour", "Tournament_TournamentId", "dbo.Tournament", "TournamentId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tour", "Tournament_TournamentId", "dbo.Tournament");
            DropIndex("dbo.Tour", new[] { "Tournament_TournamentId" });
            DropColumn("dbo.Tour", "Tournament_TournamentId");
            DropTable("dbo.Tournament");
        }
    }
}
