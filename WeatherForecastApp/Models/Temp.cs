﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherForecastApp.Models
{
    public class Temp
    {
        public int Id { get; set; }

        public double Day { get; set; }

        public double Min { get; set; }

        public double Max { get; set; }

        public double Night { get; set; }

        public double Eve { get; set; }

        public double Morn { get; set; }

        public string Date { get; set; }
    }
}