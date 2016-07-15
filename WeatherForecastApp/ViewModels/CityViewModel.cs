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
        public int CityByDefaultId { get; set; }

        [Required]
        [Display(Name = "Select days")]
        public byte DaysId { get; set; }

        [Display(Name = "Enter city")]
        public string CityName { get; set; }
    }
}