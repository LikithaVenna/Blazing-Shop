namespace Blazing_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAppointment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PersonName = c.String(),
                        Email = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        PhoneNumber = c.Long(nullable: false),
                        IsConfirmed = c.Boolean(nullable: false),
                        PId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.PId, cascadeDelete: true)
                .Index(t => t.PId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "PId", "dbo.Products");
            DropIndex("dbo.Appointments", new[] { "PId" });
            DropTable("dbo.Appointments");
        }
    }
}
