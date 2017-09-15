namespace Predictions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TourDeleteNullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tour", "TournamentId", "dbo.Tournament");
            DropIndex("dbo.Tour", new[] { "TournamentId" });
            AlterColumn("dbo.Tour", "TournamentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Tour", "TournamentId");
            AddForeignKey("dbo.Tour", "TournamentId", "dbo.Tournament", "TournamentId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tour", "TournamentId", "dbo.Tournament");
            DropIndex("dbo.Tour", new[] { "TournamentId" });
            AlterColumn("dbo.Tour", "TournamentId", c => c.Int());
            CreateIndex("dbo.Tour", "TournamentId");
            AddForeignKey("dbo.Tour", "TournamentId", "dbo.Tournament", "TournamentId");
        }
    }
}
