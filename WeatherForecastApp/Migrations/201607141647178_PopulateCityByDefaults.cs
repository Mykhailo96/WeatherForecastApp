namespace WeatherForecastApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateCityByDefaults : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO CityByDefaults (Name) VALUES ('Lviv')
                INSERT INTO CityByDefaults (Name) VALUES ('Kiev')
                INSERT INTO CityByDefaults (Name) VALUES ('Kharkiv')
                INSERT INTO CityByDefaults (Name) VALUES ('Dnipropetrovsk')
                INSERT INTO CityByDefaults (Name) VALUES ('Odessa')
                ");
        }
        
        public override void Down()
        {
        }
    }
}
