namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TourIdidentity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Match", "TourId", "dbo.Tour");
            DropPrimaryKey("dbo.Tour");
            AlterColumn("dbo.Tour", "TourId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Tour", "TourId");
            AddForeignKey("dbo.Match", "TourId", "dbo.Tour", "TourId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Match", "TourId", "dbo.Tour");
            DropPrimaryKey("dbo.Tour");
            AlterColumn("dbo.Tour", "TourId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Tour", "TourId");
            AddForeignKey("dbo.Match", "TourId", "dbo.Tour", "TourId", cascadeDelete: true);
        }
    }
}
