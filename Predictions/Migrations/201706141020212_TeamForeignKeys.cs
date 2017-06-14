namespace Predictions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TeamForeignKeys : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Match", "AwayTeam_TeamId", "dbo.Team");
            DropForeignKey("dbo.Match", "HomeTeam_TeamId", "dbo.Team");
            DropIndex("dbo.Match", new[] { "AwayTeam_TeamId" });
            DropIndex("dbo.Match", new[] { "HomeTeam_TeamId" });
            RenameColumn(table: "dbo.Match", name: "AwayTeam_TeamId", newName: "AwayTeamId");
            RenameColumn(table: "dbo.Match", name: "HomeTeam_TeamId", newName: "HomeTeamId");
            AlterColumn("dbo.Match", "AwayTeamId", c => c.Int(nullable: false));
            AlterColumn("dbo.Match", "HomeTeamId", c => c.Int(nullable: false));
            CreateIndex("dbo.Match", "HomeTeamId");
            CreateIndex("dbo.Match", "AwayTeamId");
            AddForeignKey("dbo.Match", "AwayTeamId", "dbo.Team", "TeamId", cascadeDelete: false);
            AddForeignKey("dbo.Match", "HomeTeamId", "dbo.Team", "TeamId", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Match", "HomeTeamId", "dbo.Team");
            DropForeignKey("dbo.Match", "AwayTeamId", "dbo.Team");
            DropIndex("dbo.Match", new[] { "AwayTeamId" });
            DropIndex("dbo.Match", new[] { "HomeTeamId" });
            AlterColumn("dbo.Match", "HomeTeamId", c => c.Int());
            AlterColumn("dbo.Match", "AwayTeamId", c => c.Int());
            RenameColumn(table: "dbo.Match", name: "HomeTeamId", newName: "HomeTeam_TeamId");
            RenameColumn(table: "dbo.Match", name: "AwayTeamId", newName: "AwayTeam_TeamId");
            CreateIndex("dbo.Match", "HomeTeam_TeamId");
            CreateIndex("dbo.Match", "AwayTeam_TeamId");
            AddForeignKey("dbo.Match", "HomeTeam_TeamId", "dbo.Team", "TeamId");
            AddForeignKey("dbo.Match", "AwayTeam_TeamId", "dbo.Team", "TeamId");
        }
    }
}
