using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace WeatherForecastApp.Models
{
    public class WeatherApi : IWebApi
    {
        private string url = "http://api.openweathermap.org/data/2.5/forecast/daily?APPID=d094d016c8b69124c4adb2f68b04f5b3&units=metric&q=";
        private Uri uri;
        private JsonSerializerSettings settings;

        public WeatherApi()
        {
            settings = new JsonSerializerSettings();
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        public async Task<Forecast> GetForecastAsync(string name, int days)
        {
            {
                using (var client = new WebClient())
                {
                    var json_data = string.Empty;

                    uri = new Uri(url + name + "&cnt=" + days);

                    try
                    {
                        json_data = await client.DownloadStringTaskAsync(uri);
                    }
                    catch (Exception) { }

                    return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<Forecast>(json_data, settings));
                     
                }
            }
        }
    }
}