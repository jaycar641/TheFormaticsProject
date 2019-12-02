namespace Formatics.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class iscurrent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Diagnosis", "isCurrent", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Diagnosis", "isCurrent");
        }
    }
}
