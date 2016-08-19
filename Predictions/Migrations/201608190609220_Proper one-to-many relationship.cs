namespace Predictions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Properonetomanyrelationship : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Match", "Tour_TourId", "dbo.Tour");
            DropIndex("dbo.Match", new[] { "Tour_TourId" });
            RenameColumn(table: "dbo.Match", name: "Tour_TourId", newName: "TourId");
            AlterColumn("dbo.Match", "TourId", c => c.Int(nullable: false));
            CreateIndex("dbo.Match", "TourId");
            AddForeignKey("dbo.Match", "TourId", "dbo.Tour", "TourId", cascadeDelete: true);
            DropColumn("dbo.Match", "Tour");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Match", "Tour", c => c.Int(nullable: false));
            DropForeignKey("dbo.Match", "TourId", "dbo.Tour");
            DropIndex("dbo.Match", new[] { "TourId" });
            AlterColumn("dbo.Match", "TourId", c => c.Int());
            RenameColumn(table: "dbo.Match", name: "TourId", newName: "Tour_TourId");
            CreateIndex("dbo.Match", "Tour_TourId");
            AddForeignKey("dbo.Match", "Tour_TourId", "dbo.Tour", "TourId");
        }
    }
}
