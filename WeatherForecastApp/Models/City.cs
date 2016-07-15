using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherForecastApp.Models
{
    public class City
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CoordId { get; set; }
        public Coord Coord { get; set; }

        public string Country { get; set; }

        public int Population { get; set; }

        public List<List> List { get; set; }
    }
}