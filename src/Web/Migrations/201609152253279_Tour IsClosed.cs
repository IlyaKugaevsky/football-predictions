namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TourIsClosed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tour", "IsClosed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tour", "IsClosed");
        }
    }
}
