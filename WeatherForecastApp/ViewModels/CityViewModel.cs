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
        [Display(Name = "Find your city")]
        public EnumCity NameFromEnum { get; set; }

        [Display(Name = "Select days")]
        public EnumDays DaysAmount { get; set; }

        [Display(Name = "Enter city")]
        public string CityName { get; set; }
    }
}