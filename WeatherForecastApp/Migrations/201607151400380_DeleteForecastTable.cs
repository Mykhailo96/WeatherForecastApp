namespace WeatherForecastApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteForecastTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Forecasts", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Lists", "Forecast_Id", "dbo.Forecasts");
            DropIndex("dbo.Forecasts", new[] { "CityId" });
            DropIndex("dbo.Lists", new[] { "Forecast_Id" });
            AddColumn("dbo.Lists", "City_Id", c => c.Int());
            CreateIndex("dbo.Lists", "City_Id");
            AddForeignKey("dbo.Lists", "City_Id", "dbo.Cities", "Id");
            DropColumn("dbo.Lists", "Forecast_Id");
            DropTable("dbo.Forecasts");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Lists", "Forecast_Id", c => c.Int());
            DropForeignKey("dbo.Lists", "City_Id", "dbo.Cities");
            DropIndex("dbo.Lists", new[] { "City_Id" });
            DropColumn("dbo.Lists", "City_Id");
            CreateIndex("dbo.Lists", "Forecast_Id");
            CreateIndex("dbo.Forecasts", "CityId");
            AddForeignKey("dbo.Lists", "Forecast_Id", "dbo.Forecasts", "Id");
            AddForeignKey("dbo.Forecasts", "CityId", "dbo.Cities", "Id", cascadeDelete: true);
        }
    }
}
