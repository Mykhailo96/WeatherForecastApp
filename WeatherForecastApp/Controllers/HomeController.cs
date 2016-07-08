using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeatherForecastApp.Models;
using WeatherForecastApp.ViewModels;

namespace WeatherForecastApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult City()
        {
            return View();
        }

        public ActionResult Index(CityViewModel city)
        {
            if (!ModelState.IsValid || city.NameFromEnum == EnumCity.Select && city.CityName == null)
                return View("City", city);

            string str;
            if(city.CityName == null)
            {
                str = Enum.GetName(typeof(EnumCity), city.NameFromEnum);
            }
            else
            {
                str = city.CityName;
            }
            WeatherApi api = new WeatherApi();

            return View(api.getForecast(str));
        }
    }
}