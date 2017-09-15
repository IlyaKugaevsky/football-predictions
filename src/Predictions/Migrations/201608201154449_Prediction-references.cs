namespace Predictions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Predictionreferences : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Prediction", "Expert_ExpertId", "dbo.Expert");
            DropForeignKey("dbo.Prediction", "Match_MatchId", "dbo.Match");
            DropIndex("dbo.Prediction", new[] { "Expert_ExpertId" });
            DropIndex("dbo.Prediction", new[] { "Match_MatchId" });
            RenameColumn(table: "dbo.Prediction", name: "Expert_ExpertId", newName: "ExpertId");
            RenameColumn(table: "dbo.Prediction", name: "Match_MatchId", newName: "MatchId");
            AlterColumn("dbo.Prediction", "ExpertId", c => c.Int(nullable: false));
            AlterColumn("dbo.Prediction", "MatchId", c => c.Int(nullable: false));
            CreateIndex("dbo.Prediction", "MatchId");
            CreateIndex("dbo.Prediction", "ExpertId");
            AddForeignKey("dbo.Prediction", "ExpertId", "dbo.Expert", "ExpertId", cascadeDelete: true);
            AddForeignKey("dbo.Prediction", "MatchId", "dbo.Match", "MatchId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Prediction", "MatchId", "dbo.Match");
            DropForeignKey("dbo.Prediction", "ExpertId", "dbo.Expert");
            DropIndex("dbo.Prediction", new[] { "ExpertId" });
            DropIndex("dbo.Prediction", new[] { "MatchId" });
            AlterColumn("dbo.Prediction", "MatchId", c => c.Int());
            AlterColumn("dbo.Prediction", "ExpertId", c => c.Int());
            RenameColumn(table: "dbo.Prediction", name: "MatchId", newName: "Match_MatchId");
            RenameColumn(table: "dbo.Prediction", name: "ExpertId", newName: "Expert_ExpertId");
            CreateIndex("dbo.Prediction", "Match_MatchId");
            CreateIndex("dbo.Prediction", "Expert_ExpertId");
            AddForeignKey("dbo.Prediction", "Match_MatchId", "dbo.Match", "MatchId");
            AddForeignKey("dbo.Prediction", "Expert_ExpertId", "dbo.Expert", "ExpertId");
        }
    }
}
