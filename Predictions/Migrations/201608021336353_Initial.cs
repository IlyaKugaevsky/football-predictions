namespace Predictions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Expert",
                c => new
                    {
                        ExpertId = c.Int(nullable: false, identity: true),
                        Nickname = c.String(),
                    })
                .PrimaryKey(t => t.ExpertId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Expert");
        }
    }
}
