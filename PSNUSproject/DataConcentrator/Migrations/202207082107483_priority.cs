namespace DataConcentrator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class priority : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Alarms", "HighPriority", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Alarms", "HighPriority");
        }
    }
}
