namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteTournamentToursproperty : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tour", "TournamentId", "dbo.Tournament");
            DropIndex("dbo.Tour", new[] { "TournamentId" });
            DropColumn("dbo.Tour", "TournamentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tour", "TournamentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Tour", "TournamentId");
            AddForeignKey("dbo.Tour", "TournamentId", "dbo.Tournament", "TournamentId", cascadeDelete: true);
        }
    }
}
