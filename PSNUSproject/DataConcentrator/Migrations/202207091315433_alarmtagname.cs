namespace DataConcentrator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alarmtagname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Alarms", "TagName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Alarms", "TagName");
        }
    }
}
