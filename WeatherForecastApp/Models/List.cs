using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherForecastApp.Models
{
    public class List
    {
        public int Dt { get; set; }
        public Main Main { get; set; }
        public List<Weather> Weather { get; set; }
        public Clouds Clouds { get; set; }
        public Wind Wind { get; set; }
        public Rain Rain { get; set; }
        public Sys2 Sys { get; set; }
        public string Dt_txt { get; set; }
    }
}