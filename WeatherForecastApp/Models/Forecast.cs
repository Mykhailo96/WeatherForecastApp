﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;

namespace WeatherForecastApp.Models
{
    public class Forecast
    {
        public City City { get; set; }

        public string Cod { get; set; }

        public double Message { get; set; }

        public int Cnt { get; set; }

        public List<List> List { get; set; }
    }
}