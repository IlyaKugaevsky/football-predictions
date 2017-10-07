namespace Predictions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_teams_really : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Team",
                c => new
                    {
                        TeamId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.TeamId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Team");
        }
    }
}
