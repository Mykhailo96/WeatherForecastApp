using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WeatherForecastApp.Models
{
    public enum EnumCity
    {
        Select,
        Lviv,
        Kiev,
        Kharkiv,
        Dnipropetrovsk,
        Odessa
    }

    public enum EnumDays
    {
        Today = 0,

        [Display(Name = "Three days")]
        ThreeDays = 2,

        [Display(Name = "Five days")]
        FiveDays = 4
    }
}