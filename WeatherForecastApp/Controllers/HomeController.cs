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
        private ApplicationDbContext _context;

        public HomeController(IWebApi webApi)
        {
            api = webApi;
            _context = new ApplicationDbContext();
        }

        public ActionResult City()
        {
            var cities = _context.CityByDefaults.ToList();

            ViewBag.Default = cities;

            return View();
        }

        public ActionResult Index(CityViewModel city)
        {
            if (!ModelState.IsValid)// || city.NameByDefault == EnumCity.Select && city.CityName == null)
                return View("City", city);

            string name;

            if(city.CityName == null)
            {
                name = city.CityByDefault.Name;
            }
            else
            {
                name = city.CityName;
            }

            ViewBag.Name = name;

            return View(api.getForecast(name, (int)city.DaysAmount + 1));
        }

        public ActionResult Redirect(string name, int days)
        {
            ViewBag.Name = name;

            return View("Index", api.getForecast(name, days));
        }
    }
}