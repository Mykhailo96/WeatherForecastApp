namespace WeatherForecastApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDaysTable : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO Days (Id, Name) VALUES (1, 'Today')
                INSERT INTO Days (Id, Name) VALUES (2, 'Three days')
                INSERT INTO Days (Id, Name) VALUES (3, 'Week')
                ");
        }
        
        public override void Down()
        {
        }
    }
}
