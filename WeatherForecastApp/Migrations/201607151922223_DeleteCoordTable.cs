namespace WeatherForecastApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteCoordTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cities", "CoordId", "dbo.Coords");
            DropIndex("dbo.Cities", new[] { "CoordId" });
            DropColumn("dbo.Cities", "CoordId");
            DropTable("dbo.Coords");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Coords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Lon = c.Double(nullable: false),
                        Lat = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Cities", "CoordId", c => c.Int(nullable: false));
            CreateIndex("dbo.Cities", "CoordId");
            AddForeignKey("dbo.Cities", "CoordId", "dbo.Coords", "Id", cascadeDelete: true);
        }
    }
}
