namespace Blazing_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingproductsss : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "CID", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "CID");
            AddForeignKey("dbo.Products", "CID", "dbo.Categories", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "CID", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "CID" });
            DropColumn("dbo.Products", "CID");
        }
    }
}
