﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace WeatherForecastApp.Models
{
    public class WeatherApi
    {
        private string url = "http://api.openweathermap.org/data/2.5/forecast?APPID=d094d016c8b69124c4adb2f68b04f5b3&units=metric&q=Lviv";
        private JsonSerializerSettings settings;

        public WeatherApi()
        {
            settings = new JsonSerializerSettings();
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        public Forecast getForecast()
        {
            {
                using (var client = new WebClient())
                {
                    var json_data = string.Empty;

                    try
                    {
                        json_data = client.DownloadString(url);
                    }
                    catch (Exception) { }

                    var forecast = JsonConvert.DeserializeObject<Forecast>(json_data, settings);
                    return forecast;
                }
            }
        }
    }
}