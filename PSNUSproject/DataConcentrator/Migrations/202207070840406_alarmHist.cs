namespace DataConcentrator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alarmHist : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AlarmHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AlarmID = c.Int(nullable: false),
                        VarName = c.String(),
                        Message = c.String(),
                        TimeStamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AlarmHistories");
        }
    }
}
