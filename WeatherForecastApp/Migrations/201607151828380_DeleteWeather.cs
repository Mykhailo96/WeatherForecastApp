namespace WeatherForecastApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteWeather : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Weathers", "List_Id", "dbo.Lists");
            DropIndex("dbo.Weathers", new[] { "List_Id" });
            DropTable("dbo.Weathers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Weathers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Main = c.String(),
                        Description = c.String(),
                        Icon = c.String(),
                        List_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Weathers", "List_Id");
            AddForeignKey("dbo.Weathers", "List_Id", "dbo.Lists", "Id");
        }
    }
}
