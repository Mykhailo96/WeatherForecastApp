using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WeatherForecastApp.Models;

namespace WeatherForecastApp.ViewModels
{
    public class CityViewModel
    {
        //[Display(Name = )]
        public EnumCity NameFromEnum { get; set; }

        //[Display(Name = "")]
        public string CityName { get; set; }
    }
}