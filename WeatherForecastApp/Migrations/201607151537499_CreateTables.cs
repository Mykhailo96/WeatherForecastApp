namespace WeatherForecastApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: false),
                        Name = c.String(),
                        CoordId = c.Int(nullable: false),
                        Country = c.String(),
                        Population = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Coords", t => t.CoordId, cascadeDelete: true)
                .Index(t => t.CoordId);
            
            CreateTable(
                "dbo.Weathers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: false),
                        Main = c.String(),
                        Description = c.String(),
                        Icon = c.String(),
                        List_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lists", t => t.List_Id)
                .Index(t => t.List_Id);
            
            AddColumn("dbo.Lists", "City_Id", c => c.Int());
            CreateIndex("dbo.Lists", "City_Id");
            AddForeignKey("dbo.Lists", "City_Id", "dbo.Cities", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lists", "City_Id", "dbo.Cities");
            DropForeignKey("dbo.Weathers", "List_Id", "dbo.Lists");
            DropForeignKey("dbo.Cities", "CoordId", "dbo.Coords");
            DropIndex("dbo.Weathers", new[] { "List_Id" });
            DropIndex("dbo.Lists", new[] { "City_Id" });
            DropIndex("dbo.Cities", new[] { "CoordId" });
            DropColumn("dbo.Lists", "City_Id");
            DropTable("dbo.Weathers");
            DropTable("dbo.Cities");
        }
    }
}
