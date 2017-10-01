using System.Data.Entity.Migrations;

namespace SySSensor.Core.Migrations
{
    public class _2017100101_CreateInitialTables : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.LogFile",
            //    c => new
            //    {
            //        Id = c.Int(nullable: false, identity: true),
            //        FileName = c.String(),
            //        ProcessDate = c.DateTime(),
            //        FileContent = c.String(),
            //    })
            //    .PrimaryKey(t => t.Id);

            //CreateTable(
            //    "dbo.SensorLog",
            //    c => new
            //    {
            //        Id = c.Int(nullable: false, identity: true),
            //        SensorId = c.String(),
            //        ReadDate = c.DateTime(nullable: false),
            //        Temperature = c.Double(nullable: false),
            //        Humidity = c.Double(nullable: false),
            //    })
            //    .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            //DropTable("dbo.SensorLog");
            //DropTable("dbo.LogFile");
        }
    }
}
