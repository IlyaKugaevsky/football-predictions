namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wtf : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Match", "NewTourId", "dbo.NewTour");
            DropIndex("dbo.Match", new[] { "NewTourId" });
            AlterColumn("dbo.Match", "NewTourId", c => c.Int());
            CreateIndex("dbo.Match", "NewTourId");
            AddForeignKey("dbo.Match", "NewTourId", "dbo.NewTour", "NewTourId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Match", "NewTourId", "dbo.NewTour");
            DropIndex("dbo.Match", new[] { "NewTourId" });
            AlterColumn("dbo.Match", "NewTourId", c => c.Int(nullable: false));
            CreateIndex("dbo.Match", "NewTourId");
            AddForeignKey("dbo.Match", "NewTourId", "dbo.NewTour", "NewTourId", cascadeDelete: true);
        }
    }
}
