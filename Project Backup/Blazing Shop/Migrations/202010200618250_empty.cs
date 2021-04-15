namespace Blazing_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class empty : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Appointments", "PersonName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Appointments", "PersonName", c => c.String());
        }
    }
}
