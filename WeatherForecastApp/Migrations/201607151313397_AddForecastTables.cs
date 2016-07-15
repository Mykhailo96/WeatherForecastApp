namespace WeatherForecastApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForecastTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CoordId = c.Int(nullable: false),
                        Country = c.String(),
                        Population = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Coords", t => t.CoordId, cascadeDelete: true)
                .Index(t => t.CoordId);
            
            CreateTable(
                "dbo.Coords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Lon = c.Double(nullable: false),
                        Lat = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Forecasts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CityId = c.Int(nullable: false),
                        Cod = c.String(),
                        Message = c.Double(nullable: false),
                        Cnt = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.Lists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Dt = c.Int(nullable: false),
                        TempId = c.Int(nullable: false),
                        Pressure = c.Double(nullable: false),
                        Humidity = c.Int(nullable: false),
                        Speed = c.Double(nullable: false),
                        Deg = c.Int(nullable: false),
                        Clouds = c.Int(nullable: false),
                        Rain = c.Double(nullable: false),
                        Forecast_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Temps", t => t.TempId, cascadeDelete: true)
                .ForeignKey("dbo.Forecasts", t => t.Forecast_Id)
                .Index(t => t.TempId)
                .Index(t => t.Forecast_Id);
            
            CreateTable(
                "dbo.Temps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Day = c.Double(nullable: false),
                        Min = c.Double(nullable: false),
                        Max = c.Double(nullable: false),
                        Night = c.Double(nullable: false),
                        Eve = c.Double(nullable: false),
                        Morn = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Weathers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Main = c.String(),
                        Description = c.String(),
                        Icon = c.String(),
                        List_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lists", t => t.List_Id)
                .Index(t => t.List_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lists", "Forecast_Id", "dbo.Forecasts");
            DropForeignKey("dbo.Weathers", "List_Id", "dbo.Lists");
            DropForeignKey("dbo.Lists", "TempId", "dbo.Temps");
            DropForeignKey("dbo.Forecasts", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Cities", "CoordId", "dbo.Coords");
            DropIndex("dbo.Weathers", new[] { "List_Id" });
            DropIndex("dbo.Lists", new[] { "Forecast_Id" });
            DropIndex("dbo.Lists", new[] { "TempId" });
            DropIndex("dbo.Forecasts", new[] { "CityId" });
            DropIndex("dbo.Cities", new[] { "CoordId" });
            DropTable("dbo.Weathers");
            DropTable("dbo.Temps");
            DropTable("dbo.Lists");
            DropTable("dbo.Forecasts");
            DropTable("dbo.Coords");
            DropTable("dbo.Cities");
        }
    }
}
