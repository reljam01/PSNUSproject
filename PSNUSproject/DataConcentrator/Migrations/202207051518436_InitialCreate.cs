namespace DataConcentrator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Alarms",
                c => new
                    {
                        Id = c.Double(nullable: false),
                        Name = c.String(),
                        Value = c.Double(nullable: false),
                        OnUpperVal = c.Boolean(nullable: false),
                        Message = c.String(),
                        Activated = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AnalogInputs",
                c => new
                    {
                        ID = c.Double(nullable: false),
                        Value = c.Double(nullable: false),
                        LowLimit = c.Double(nullable: false),
                        HighLimit = c.Double(nullable: false),
                        Units = c.String(),
                        ScanTime = c.Double(nullable: false),
                        OnOffScan = c.Boolean(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AnalogOutputs",
                c => new
                    {
                        ID = c.Double(nullable: false),
                        Value = c.Double(nullable: false),
                        LowLimit = c.Double(nullable: false),
                        HighLimit = c.Double(nullable: false),
                        Units = c.String(),
                        InitialValue = c.Double(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.DigitalInputs",
                c => new
                    {
                        ID = c.Double(nullable: false),
                        Value = c.Boolean(nullable: false),
                        ScanTime = c.Double(nullable: false),
                        OnOffScan = c.Boolean(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.DigitalOutputs",
                c => new
                    {
                        ID = c.Double(nullable: false),
                        Value = c.Boolean(nullable: false),
                        InitialValue = c.Double(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DigitalOutputs");
            DropTable("dbo.DigitalInputs");
            DropTable("dbo.AnalogOutputs");
            DropTable("dbo.AnalogInputs");
            DropTable("dbo.Alarms");
        }
    }
}
