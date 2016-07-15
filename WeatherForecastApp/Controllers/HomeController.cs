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

            int days = _context.Days.SingleOrDefault(d => d.Id == city.DaysId).Number;

            Forecast forecast = api.getForecast(name, days);

            var cityInDb = _context.Cities.SingleOrDefault(c => c.Id == forecast.City.Id);

            if (cityInDb == null)
            {
                var coord = _context.Coords.Add(forecast.City.Coord);

                forecast.City.CoordId = coord.Id;
                cityInDb = _context.Cities.Add(forecast.City);
            }

            _context.SaveChanges();


            foreach (var day in forecast.List)
            {

                var dayInDb = _context.Lists.SingleOrDefault(d => d.Dt == day.Dt && d.CityId == forecast.City.Id);

                if (dayInDb == null)
                {
                    var newTemp = _context.Temps.Add(day.Temp);

                    _context.SaveChanges();

                    day.TempId = newTemp.Id;
                    day.CityId = cityInDb.Id;

                    var newDay = _context.Lists.Add(day);

                    cityInDb.List.Add(newDay);

                    _context.SaveChanges();
                }
            }

            return View(forecast);
        }

        public ActionResult Redirect(string name, int days)
        {
            ViewBag.Name = name;

            Forecast forecast = api.getForecast(name, days);

            var cityInDb = _context.Cities.SingleOrDefault(c => c.Id == forecast.City.Id);

            foreach (var day in forecast.List)
            {

                var dayInDb = _context.Lists.SingleOrDefault(d => d.Dt == day.Dt && d.CityId == forecast.City.Id);

                if (dayInDb == null)
                {
                    var newTemp = _context.Temps.Add(day.Temp);

                    _context.SaveChanges();

                    day.TempId = newTemp.Id;
                    day.CityId = cityInDb.Id;

                    var newDay = _context.Lists.Add(day);

                    cityInDb.List.Add(newDay);

                    _context.SaveChanges();
                }  
            }           

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