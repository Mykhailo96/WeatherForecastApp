﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecastApp.Models
{
    public interface IWebApi
    {
        Forecast getForecast(string name);
    }
}
