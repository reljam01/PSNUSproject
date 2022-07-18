namespace DataConcentrator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newmigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Alarms", "TagId", c => c.Int(nullable: false));
            CreateIndex("dbo.Alarms", "TagId");
            AddForeignKey("dbo.Alarms", "TagId", "dbo.AnalogInputs", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Alarms", "TagId", "dbo.AnalogInputs");
            DropIndex("dbo.Alarms", new[] { "TagId" });
            DropColumn("dbo.Alarms", "TagId");
        }
    }
}
