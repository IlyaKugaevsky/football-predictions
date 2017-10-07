namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewTourrelations : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Match", "NewTour_NewTourId", "dbo.NewTour");
            DropIndex("dbo.Match", new[] { "NewTour_NewTourId" });
            RenameColumn(table: "dbo.Match", name: "NewTour_NewTourId", newName: "NewTourId");
            AlterColumn("dbo.Match", "NewTourId", c => c.Int(nullable: true));
            CreateIndex("dbo.Match", "NewTourId");
            AddForeignKey("dbo.Match", "NewTourId", "dbo.NewTour", "NewTourId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Match", "NewTourId", "dbo.NewTour");
            DropIndex("dbo.Match", new[] { "NewTourId" });
            AlterColumn("dbo.Match", "NewTourId", c => c.Int());
            RenameColumn(table: "dbo.Match", name: "NewTourId", newName: "NewTour_NewTourId");
            CreateIndex("dbo.Match", "NewTour_NewTourId");
            AddForeignKey("dbo.Match", "NewTour_NewTourId", "dbo.NewTour", "NewTourId");
        }
    }
}
