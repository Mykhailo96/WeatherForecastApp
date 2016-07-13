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
        Today,

        [Display(Name = "Three days")]
        ThreeDays,

        [Display(Name = "Five days")]
        FiveDays
    }
}