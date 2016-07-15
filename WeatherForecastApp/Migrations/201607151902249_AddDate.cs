namespace WeatherForecastApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Temps", "Date", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Temps", "Date");
        }
    }
}
