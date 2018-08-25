namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HeadToHead : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HeadToHeadMatch",
                c => new
                    {
                        HeadToHeadMatchId = c.Int(nullable: false, identity: true),
                        HomeExpertId = c.Int(nullable: false),
                        AwayExpertId = c.Int(nullable: false),
                        HomeGoals = c.Byte(nullable: false),
                        AwayGoals = c.Byte(nullable: false),
                        IsOver = c.Boolean(nullable: false),
                        HeadToHeadTournament_HeadToHeadTournamentId = c.Int(),
                        HeadToHeadTour_HeadToHeadTourId = c.Int(),
                    })
                .PrimaryKey(t => t.HeadToHeadMatchId)
                .ForeignKey("dbo.Expert", t => t.AwayExpertId, cascadeDelete: false)
                .ForeignKey("dbo.Expert", t => t.HomeExpertId, cascadeDelete: false)
                .ForeignKey("dbo.HeadToHeadTournament", t => t.HeadToHeadTournament_HeadToHeadTournamentId)
                .ForeignKey("dbo.HeadToHeadTour", t => t.HeadToHeadTour_HeadToHeadTourId)
                .Index(t => t.HomeExpertId)
                .Index(t => t.AwayExpertId)
                .Index(t => t.HeadToHeadTournament_HeadToHeadTournamentId)
                .Index(t => t.HeadToHeadTour_HeadToHeadTourId);
            
            CreateTable(
                "dbo.HeadToHeadTournament",
                c => new
                    {
                        HeadToHeadTournamentId = c.Int(nullable: false, identity: true),
                        ParentTournamentId = c.Int(nullable: false),
                        ParentTournament_TournamentId = c.Int(),
                    })
                .PrimaryKey(t => t.HeadToHeadTournamentId)
                .ForeignKey("dbo.Tournament", t => t.ParentTournament_TournamentId)
                .Index(t => t.ParentTournament_TournamentId);
            
            CreateTable(
                "dbo.HeadToHeadTour",
                c => new
                    {
                        HeadToHeadTourId = c.Int(nullable: false, identity: true),
                        ParentTourId = c.Int(nullable: false),
                        ParentTour_TourId = c.Int(),
                    })
                .PrimaryKey(t => t.HeadToHeadTourId)
                .ForeignKey("dbo.Tour", t => t.ParentTour_TourId)
                .Index(t => t.ParentTour_TourId);
            
            AddColumn("dbo.OldTour", "IsPlayoff", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HeadToHeadTour", "ParentTour_TourId", "dbo.Tour");
            DropForeignKey("dbo.HeadToHeadMatch", "HeadToHeadTour_HeadToHeadTourId", "dbo.HeadToHeadTour");
            DropForeignKey("dbo.HeadToHeadTournament", "ParentTournament_TournamentId", "dbo.Tournament");
            DropForeignKey("dbo.HeadToHeadMatch", "HeadToHeadTournament_HeadToHeadTournamentId", "dbo.HeadToHeadTournament");
            DropForeignKey("dbo.HeadToHeadMatch", "HomeExpertId", "dbo.Expert");
            DropForeignKey("dbo.HeadToHeadMatch", "AwayExpertId", "dbo.Expert");
            DropIndex("dbo.HeadToHeadTour", new[] { "ParentTour_TourId" });
            DropIndex("dbo.HeadToHeadTournament", new[] { "ParentTournament_TournamentId" });
            DropIndex("dbo.HeadToHeadMatch", new[] { "HeadToHeadTour_HeadToHeadTourId" });
            DropIndex("dbo.HeadToHeadMatch", new[] { "HeadToHeadTournament_HeadToHeadTournamentId" });
            DropIndex("dbo.HeadToHeadMatch", new[] { "AwayExpertId" });
            DropIndex("dbo.HeadToHeadMatch", new[] { "HomeExpertId" });
            DropColumn("dbo.OldTour", "IsPlayoff");
            DropTable("dbo.HeadToHeadTour");
            DropTable("dbo.HeadToHeadTournament");
            DropTable("dbo.HeadToHeadMatch");
        }
    }
}
