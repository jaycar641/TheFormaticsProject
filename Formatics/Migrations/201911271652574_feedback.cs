namespace Formatics.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class feedback : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Feedbacks", "type", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Feedbacks", "type");
        }
    }
}
