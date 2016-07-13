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
        private IWebApi api;

        public HomeController(IWebApi webApi)
        {
            api = webApi;
        }

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

            return View(api.getForecast(str, (int)city.DaysAmount + 1));
        }
    }
}