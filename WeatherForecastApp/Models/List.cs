﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WeatherForecastApp.Models
{
    public class List
    {
        public int Id { get; set; }

        public int Dt { get; set; }

        public int TempId { get; set; }
        public Temp Temp { get; set; }

        public double Pressure { get; set; }

        public int Humidity { get; set; }

        [NotMapped]
        public List<Weather> Weather { get; set; }

        public double Speed { get; set; }

        public int Deg { get; set; }

        public int Clouds { get; set; }

        public double Rain { get; set; }

        public int CityId { get; set; }
    }
}