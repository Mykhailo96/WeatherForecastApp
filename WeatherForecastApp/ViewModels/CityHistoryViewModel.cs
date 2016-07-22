using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WeatherForecastApp.ViewModels
{
    public class CityHistoryViewModel
    {
        [Display(Name = "Find your city")]
        public int CityByDefaultId { get; set; }

        [Display(Name = "Enter city")]
        public string CityName { get; set; }
    }
}