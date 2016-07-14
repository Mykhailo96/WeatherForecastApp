using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WeatherForecastApp.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<CityByDefault> CityByDefaults { get; set; }

        public DbSet<Days> Days { get; set; }

        public ApplicationDbContext() : base("WeatherForecastApp")
        {
        }
    }
}