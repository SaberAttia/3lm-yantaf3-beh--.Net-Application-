namespace YontafaeOBih.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplyContents1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ApplyForThisContents", new[] { "userId" });
            CreateIndex("dbo.ApplyForThisContents", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ApplyForThisContents", new[] { "UserId" });
            CreateIndex("dbo.ApplyForThisContents", "userId");
        }
    }
}
