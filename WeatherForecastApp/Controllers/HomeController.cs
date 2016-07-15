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
            var days = _context.Days.ToList();

            ViewBag.Default = cities;
            ViewBag.Days = days;

            return View();
        }

        public ActionResult Index(CityViewModel city)
         {
            if (!ModelState.IsValid)
                return View("City", city);

            string name;

            if(city.CityName == null)
            {
                name = _context.CityByDefaults.SingleOrDefault(c => c.Id == city.CityByDefaultId).Name;
            }
            else
            {
                name = city.CityName;
            }

            ViewBag.Name = name;

            int num = _context.Days.SingleOrDefault(d => d.Id == city.DaysId).Number;

            return View(api.getForecast(name, num));
        }

        public ActionResult Redirect(string name, int days)
        {
            ViewBag.Name = name;

            Forecast forecast = api.getForecast(name, days);

            //var city = _context.Cities.SingleOrDefault(c => c.Id == forecast.CityId);

            //if (city != null)
            //{
            //    _context.Cities.Add(forecast.City);
            //}

            //_context.Coords.Add(forecast.City.Coord);

            //foreach (var day in forecast.List)
            //{
            //    _context.Lists.Add(day);
            //    _context.Temps.Add(day.Temp);
            //    _context.Weathers.Add(day.Weather[0]);
            //}

            return View("Index", forecast);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}