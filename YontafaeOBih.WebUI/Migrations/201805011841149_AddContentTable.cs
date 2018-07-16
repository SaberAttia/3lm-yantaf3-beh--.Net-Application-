namespace YontafaeOBih.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddContentTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContentTitle = c.String(),
                        ContentDescription = c.String(),
                        ContentImage = c.String(),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contents", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Contents", new[] { "CategoryId" });
            DropTable("dbo.Contents");
        }
    }
}
