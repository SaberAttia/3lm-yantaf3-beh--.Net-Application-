namespace YontafaeOBih.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplyContents : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplyForThisContents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        ApplyDate = c.DateTime(nullable: false),
                        ContentId = c.Int(nullable: false),
                        userId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contents", t => t.ContentId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.userId)
                .Index(t => t.ContentId)
                .Index(t => t.userId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplyForThisContents", "userId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplyForThisContents", "ContentId", "dbo.Contents");
            DropIndex("dbo.ApplyForThisContents", new[] { "userId" });
            DropIndex("dbo.ApplyForThisContents", new[] { "ContentId" });
            DropTable("dbo.ApplyForThisContents");
        }
    }
}
