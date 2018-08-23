namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Playoff : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Prediction", "PlayoffWinner", c => c.Int());
            AddColumn("dbo.Match", "PlayoffWinner", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Match", "PlayoffWinner");
            DropColumn("dbo.Prediction", "PlayoffWinner");
        }
    }
}
