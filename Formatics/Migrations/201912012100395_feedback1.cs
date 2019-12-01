namespace Formatics.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class feedback1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Feedbacks", "date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Feedbacks", "date");
        }
    }
}
