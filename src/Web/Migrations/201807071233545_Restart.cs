namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Restart : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Expert",
            //    c => new
            //        {
            //            ExpertId = c.Int(nullable: false, identity: true),
            //            Nickname = c.String(),
            //            Outcomes = c.Int(nullable: false),
            //            Differences = c.Int(nullable: false),
            //            Scores = c.Int(nullable: false),
            //            Sum = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.ExpertId);
            
            //CreateTable(
            //    "dbo.Prediction",
            //    c => new
            //        {
            //            PredictionId = c.Int(nullable: false, identity: true),
            //            Value = c.String(),
            //            Sum = c.Int(nullable: false),
            //            Score = c.Boolean(nullable: false),
            //            Difference = c.Boolean(nullable: false),
            //            Outcome = c.Boolean(nullable: false),
            //            IsClosed = c.Boolean(nullable: false),
            //            MatchId = c.Int(nullable: false),
            //            ExpertId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.PredictionId)
            //    .ForeignKey("dbo.Expert", t => t.ExpertId, cascadeDelete: true)
            //    .ForeignKey("dbo.Match", t => t.MatchId, cascadeDelete: true)
            //    .Index(t => t.MatchId)
            //    .Index(t => t.ExpertId);
            
            //CreateTable(
            //    "dbo.Match",
            //    c => new
            //        {
            //            MatchId = c.Int(nullable: false, identity: true),
            //            Title = c.String(),
            //            Score = c.String(),
            //            Date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
            //            HomeTeamId = c.Int(nullable: false),
            //            AwayTeamId = c.Int(nullable: false),
            //            TourId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.MatchId)
            //    .ForeignKey("dbo.Team", t => t.AwayTeamId, cascadeDelete: true)
            //    .ForeignKey("dbo.Team", t => t.HomeTeamId, cascadeDelete: true)
            //    .ForeignKey("dbo.Tour", t => t.TourId, cascadeDelete: true)
            //    .Index(t => t.HomeTeamId)
            //    .Index(t => t.AwayTeamId)
            //    .Index(t => t.TourId);
            
            //CreateTable(
            //    "dbo.Team",
            //    c => new
            //        {
            //            TeamId = c.Int(nullable: false, identity: true),
            //            Title = c.String(),
            //        })
            //    .PrimaryKey(t => t.TeamId);
            
            //CreateTable(
            //    "dbo.Tour",
            //    c => new
            //        {
            //            TourId = c.Int(nullable: false, identity: true),
            //            TournamentId = c.Int(nullable: false),
            //            TourNumber = c.Int(nullable: false),
            //            IsClosed = c.Boolean(nullable: false),
            //            StartDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
            //            EndDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
            //        })
            //    .PrimaryKey(t => t.TourId)
            //    .ForeignKey("dbo.Tournament", t => t.TournamentId, cascadeDelete: true)
            //    .Index(t => t.TournamentId);
            
            //CreateTable(
            //    "dbo.Tournament",
            //    c => new
            //        {
            //            TournamentId = c.Int(nullable: false, identity: true),
            //            Title = c.String(),
            //            StartDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
            //            EndDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
            //        })
            //    .PrimaryKey(t => t.TournamentId);
            
            //CreateTable(
            //    "dbo.OldTour",
            //    c => new
            //        {
            //            OldTourId = c.Int(nullable: false, identity: true),
            //            OldTourNumber = c.Int(nullable: false),
            //            IsClosed = c.Boolean(nullable: false),
            //            StartDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
            //            EndDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
            //        })
            //    .PrimaryKey(t => t.OldTourId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tour", "TournamentId", "dbo.Tournament");
            DropForeignKey("dbo.Match", "TourId", "dbo.Tour");
            DropForeignKey("dbo.Prediction", "MatchId", "dbo.Match");
            DropForeignKey("dbo.Match", "HomeTeamId", "dbo.Team");
            DropForeignKey("dbo.Match", "AwayTeamId", "dbo.Team");
            DropForeignKey("dbo.Prediction", "ExpertId", "dbo.Expert");
            DropIndex("dbo.Tour", new[] { "TournamentId" });
            DropIndex("dbo.Match", new[] { "TourId" });
            DropIndex("dbo.Match", new[] { "AwayTeamId" });
            DropIndex("dbo.Match", new[] { "HomeTeamId" });
            DropIndex("dbo.Prediction", new[] { "ExpertId" });
            DropIndex("dbo.Prediction", new[] { "MatchId" });
            DropTable("dbo.OldTour");
            DropTable("dbo.Tournament");
            DropTable("dbo.Tour");
            DropTable("dbo.Team");
            DropTable("dbo.Match");
            DropTable("dbo.Prediction");
            DropTable("dbo.Expert");
        }
    }
}
