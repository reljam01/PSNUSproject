namespace DataConcentrator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intid : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Alarms");
            DropPrimaryKey("dbo.AnalogInputs");
            DropPrimaryKey("dbo.AnalogOutputs");
            DropPrimaryKey("dbo.DigitalInputs");
            DropPrimaryKey("dbo.DigitalOutputs");
            AlterColumn("dbo.Alarms", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.AnalogInputs", "ID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.AnalogOutputs", "ID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.DigitalInputs", "ID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.DigitalOutputs", "ID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Alarms", "Id");
            AddPrimaryKey("dbo.AnalogInputs", "ID");
            AddPrimaryKey("dbo.AnalogOutputs", "ID");
            AddPrimaryKey("dbo.DigitalInputs", "ID");
            AddPrimaryKey("dbo.DigitalOutputs", "ID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.DigitalOutputs");
            DropPrimaryKey("dbo.DigitalInputs");
            DropPrimaryKey("dbo.AnalogOutputs");
            DropPrimaryKey("dbo.AnalogInputs");
            DropPrimaryKey("dbo.Alarms");
            AlterColumn("dbo.DigitalOutputs", "ID", c => c.Double(nullable: false));
            AlterColumn("dbo.DigitalInputs", "ID", c => c.Double(nullable: false));
            AlterColumn("dbo.AnalogOutputs", "ID", c => c.Double(nullable: false));
            AlterColumn("dbo.AnalogInputs", "ID", c => c.Double(nullable: false));
            AlterColumn("dbo.Alarms", "Id", c => c.Double(nullable: false));
            AddPrimaryKey("dbo.DigitalOutputs", "ID");
            AddPrimaryKey("dbo.DigitalInputs", "ID");
            AddPrimaryKey("dbo.AnalogOutputs", "ID");
            AddPrimaryKey("dbo.AnalogInputs", "ID");
            AddPrimaryKey("dbo.Alarms", "Id");
        }
    }
}
