namespace Formatics.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class medicatoin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Medicines", "startDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Medicines", "endDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Medicines", "endDate");
            DropColumn("dbo.Medicines", "startDate");
        }
    }
}
