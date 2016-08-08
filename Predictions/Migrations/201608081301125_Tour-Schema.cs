namespace Predictions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TourSchema : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tour",
                c => new
                    {
                        TourId = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        EndDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.TourId);
            
            AddColumn("dbo.Match", "Tour_TourId", c => c.Int());
            CreateIndex("dbo.Match", "Tour_TourId");
            AddForeignKey("dbo.Match", "Tour_TourId", "dbo.Tour", "TourId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Match", "Tour_TourId", "dbo.Tour");
            DropIndex("dbo.Match", new[] { "Tour_TourId" });
            DropColumn("dbo.Match", "Tour_TourId");
            DropTable("dbo.Tour");
        }
    }
}
