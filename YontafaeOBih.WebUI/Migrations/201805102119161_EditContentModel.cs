namespace YontafaeOBih.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditContentModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contents", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Contents", "UserId");
            AddForeignKey("dbo.Contents", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contents", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Contents", new[] { "UserId" });
            DropColumn("dbo.Contents", "UserId");
        }
    }
}
