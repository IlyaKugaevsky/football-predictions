namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HeadToHeadFix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.HeadToHeadMatch", "HeadToHeadTournament_HeadToHeadTournamentId", "dbo.HeadToHeadTournament");
            DropIndex("dbo.HeadToHeadMatch", new[] { "HeadToHeadTournament_HeadToHeadTournamentId" });
            AddColumn("dbo.HeadToHeadTour", "HeadToHeadTournament_HeadToHeadTournamentId", c => c.Int());
            CreateIndex("dbo.HeadToHeadTour", "HeadToHeadTournament_HeadToHeadTournamentId");
            AddForeignKey("dbo.HeadToHeadTour", "HeadToHeadTournament_HeadToHeadTournamentId", "dbo.HeadToHeadTournament", "HeadToHeadTournamentId");
            DropColumn("dbo.HeadToHeadMatch", "HeadToHeadTournament_HeadToHeadTournamentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.HeadToHeadMatch", "HeadToHeadTournament_HeadToHeadTournamentId", c => c.Int());
            DropForeignKey("dbo.HeadToHeadTour", "HeadToHeadTournament_HeadToHeadTournamentId", "dbo.HeadToHeadTournament");
            DropIndex("dbo.HeadToHeadTour", new[] { "HeadToHeadTournament_HeadToHeadTournamentId" });
            DropColumn("dbo.HeadToHeadTour", "HeadToHeadTournament_HeadToHeadTournamentId");
            CreateIndex("dbo.HeadToHeadMatch", "HeadToHeadTournament_HeadToHeadTournamentId");
            AddForeignKey("dbo.HeadToHeadMatch", "HeadToHeadTournament_HeadToHeadTournamentId", "dbo.HeadToHeadTournament", "HeadToHeadTournamentId");
        }
    }
}
