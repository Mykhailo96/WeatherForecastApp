namespace WeatherForecastApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddNumberForDays : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Days", "Number", c => c.Byte(nullable: false));

            Sql(@"UPDATE Days SET Number = 1 WHERE id = 1
                  UPDATE Days SET Number = 3 WHERE id = 2
                  UPDATE Days SET Number = 7 WHERE id = 3
                ");
        }

        public override void Down()
        {
            DropColumn("dbo.Days", "Number");
        }
    }
}
