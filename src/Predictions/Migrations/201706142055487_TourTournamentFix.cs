namespace Predictions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TourTournamentFix : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Tour", name: "Tournament_TournamentId", newName: "TournamentId");
            RenameIndex(table: "dbo.Tour", name: "IX_Tournament_TournamentId", newName: "IX_TournamentId");
            AddColumn("dbo.Tour", "TourNumber", c => c.Int(nullable: false));
            AddColumn("dbo.Tournament", "Title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tournament", "Title");
            DropColumn("dbo.Tour", "TourNumber");
            RenameIndex(table: "dbo.Tour", name: "IX_TournamentId", newName: "IX_Tournament_TournamentId");
            RenameColumn(table: "dbo.Tour", name: "TournamentId", newName: "Tournament_TournamentId");
        }
    }
}
