namespace WeatherForecastApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropTables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cities", "CoordId", "dbo.Coords");
            DropForeignKey("dbo.Weathers", "List_Id", "dbo.Lists");
            DropForeignKey("dbo.Lists", "City_Id", "dbo.Cities");
            DropIndex("dbo.Cities", new[] { "CoordId" });
            DropIndex("dbo.Lists", new[] { "City_Id" });
            DropIndex("dbo.Weathers", new[] { "List_Id" });
            DropColumn("dbo.Lists", "City_Id");
            DropTable("dbo.Cities");
            DropTable("dbo.Weathers");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.Id);
            
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
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Lists", "City_Id", c => c.Int());
            CreateIndex("dbo.Weathers", "List_Id");
            CreateIndex("dbo.Lists", "City_Id");
            CreateIndex("dbo.Cities", "CoordId");
            AddForeignKey("dbo.Lists", "City_Id", "dbo.Cities", "Id");
            AddForeignKey("dbo.Weathers", "List_Id", "dbo.Lists", "Id");
            AddForeignKey("dbo.Cities", "CoordId", "dbo.Coords", "Id", cascadeDelete: true);
        }
    }
}
