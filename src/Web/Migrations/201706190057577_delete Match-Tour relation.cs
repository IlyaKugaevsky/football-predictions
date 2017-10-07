namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteMatchTourrelation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Match", "TourId", "dbo.Tour");
            DropIndex("dbo.Match", new[] { "TourId" });
            DropColumn("dbo.Match", "TourId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Match", "TourId", c => c.Int(nullable: false));
            CreateIndex("dbo.Match", "TourId");
            AddForeignKey("dbo.Match", "TourId", "dbo.Tour", "TourId", cascadeDelete: true);
        }
    }
}
