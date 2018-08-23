namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PlayoffTour : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Prediction", "IsPlayoff", c => c.Boolean(nullable: false));
            AddColumn("dbo.Tour", "IsPlayoff", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tour", "IsPlayoff");
            DropColumn("dbo.Prediction", "IsPlayoff");
        }
    }
}
