using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeatherForecastApp.Models;

namespace WeatherForecastApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult City()
        {
            return View();
        }

        public ActionResult Index()
        {
            WeatherApi api = new WeatherApi();

            return View(api.getForecast());
        }
    }
}