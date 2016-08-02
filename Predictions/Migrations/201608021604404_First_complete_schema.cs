namespace Predictions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First_complete_schema : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Prediction",
                c => new
                    {
                        PredictionId = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                        Outcome = c.Boolean(),
                        Difference = c.Boolean(),
                        Score = c.Boolean(),
                        Sum = c.Int(),
                        Expert_ExpertId = c.Int(),
                        Match_MatchId = c.Int(),
                    })
                .PrimaryKey(t => t.PredictionId)
                .ForeignKey("dbo.Expert", t => t.Expert_ExpertId)
                .ForeignKey("dbo.Match", t => t.Match_MatchId)
                .Index(t => t.Expert_ExpertId)
                .Index(t => t.Match_MatchId);
            
            CreateTable(
                "dbo.Match",
                c => new
                    {
                        MatchId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Score = c.String(),
                        Tour = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        AwayTeam_TeamId = c.Int(),
                        HomeTeam_TeamId = c.Int(),
                    })
                .PrimaryKey(t => t.MatchId)
                .ForeignKey("dbo.Team", t => t.AwayTeam_TeamId)
                .ForeignKey("dbo.Team", t => t.HomeTeam_TeamId)
                .Index(t => t.AwayTeam_TeamId)
                .Index(t => t.HomeTeam_TeamId);
            
            AddColumn("dbo.Expert", "Outcomes", c => c.Int(nullable: false));
            AddColumn("dbo.Expert", "Differences", c => c.Int(nullable: false));
            AddColumn("dbo.Expert", "Scores", c => c.Int(nullable: false));
            AddColumn("dbo.Expert", "Sum", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Prediction", "Match_MatchId", "dbo.Match");
            DropForeignKey("dbo.Match", "HomeTeam_TeamId", "dbo.Team");
            DropForeignKey("dbo.Match", "AwayTeam_TeamId", "dbo.Team");
            DropForeignKey("dbo.Prediction", "Expert_ExpertId", "dbo.Expert");
            DropIndex("dbo.Match", new[] { "HomeTeam_TeamId" });
            DropIndex("dbo.Match", new[] { "AwayTeam_TeamId" });
            DropIndex("dbo.Prediction", new[] { "Match_MatchId" });
            DropIndex("dbo.Prediction", new[] { "Expert_ExpertId" });
            DropColumn("dbo.Expert", "Sum");
            DropColumn("dbo.Expert", "Scores");
            DropColumn("dbo.Expert", "Differences");
            DropColumn("dbo.Expert", "Outcomes");
            DropTable("dbo.Match");
            DropTable("dbo.Prediction");
        }
    }
}
