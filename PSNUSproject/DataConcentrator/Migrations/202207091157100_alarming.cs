namespace DataConcentrator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alarming : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AnalogInputs", "Alarming", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AnalogInputs", "Alarming");
        }
    }
}
