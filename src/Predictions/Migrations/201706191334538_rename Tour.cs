namespace Predictions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renameTour : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OldTour",
                c => new
                    {
                        OldTourId = c.Int(nullable: false, identity: true),
                        OldTourNumber = c.Int(nullable: false),
                        IsClosed = c.Boolean(nullable: false),
                        StartDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        EndDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.OldTourId);
            
            DropTable("dbo.Tour");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Tour",
                c => new
                    {
                        TourId = c.Int(nullable: false, identity: true),
                        TourNumber = c.Int(nullable: false),
                        IsClosed = c.Boolean(nullable: false),
                        StartDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        EndDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.TourId);
            
            DropTable("dbo.OldTour");
        }
    }
}
