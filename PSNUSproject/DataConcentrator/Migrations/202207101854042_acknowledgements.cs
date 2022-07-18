namespace DataConcentrator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class acknowledgements : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AlarmHistories", "Acknowledged", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AlarmHistories", "Acknowledged");
        }
    }
}
