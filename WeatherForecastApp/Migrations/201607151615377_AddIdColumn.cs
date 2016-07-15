namespace WeatherForecastApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIdColumn : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Lists", "City_Id", "dbo.Cities");
            DropIndex("dbo.Lists", new[] { "City_Id" });
            RenameColumn(table: "dbo.Lists", name: "City_Id", newName: "CityId");
            AlterColumn("dbo.Lists", "CityId", c => c.Int(nullable: false));
            CreateIndex("dbo.Lists", "CityId");
            AddForeignKey("dbo.Lists", "CityId", "dbo.Cities", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lists", "CityId", "dbo.Cities");
            DropIndex("dbo.Lists", new[] { "CityId" });
            AlterColumn("dbo.Lists", "CityId", c => c.Int());
            RenameColumn(table: "dbo.Lists", name: "CityId", newName: "City_Id");
            CreateIndex("dbo.Lists", "City_Id");
            AddForeignKey("dbo.Lists", "City_Id", "dbo.Cities", "Id");
        }
    }
}
