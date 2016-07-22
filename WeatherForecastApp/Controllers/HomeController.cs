using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeatherForecastApp.Models;
using WeatherForecastApp.ViewModels;
using System.Data.Entity;
using WeatherForecastApp.Services;

namespace WeatherForecastApp.Controllers
{
    public class HomeController : Controller
    {
        private IWebApi api;
        private IHomeService _service;

        public HomeController(IWebApi webApi, IHomeService service)
        {
            api = webApi;
            _service = service;
        }

        public ActionResult City()
        {
            ViewBag.Default = _service.CitiesByDefaultList();
            ViewBag.Days = _service.DaysList();

            return View();
        }

        public ActionResult Index(CityViewModel city)
         {
            if (!ModelState.IsValid)
                return View("City", city);

            string name;

            if(city.CityName == null)
            {
                name = _service.GetCityNameById(city.CityByDefaultId);
            }
            else
            {
                name = city.CityName;
            }

            ViewBag.Name = name;

            int days = _service.GetDaysNumberById(city.DaysId);

            Forecast forecast = api.getForecast(name, days);

            return View(_service.GetForecast(forecast));
        }

        public ActionResult Redirect(string name, int days)
        {
            ViewBag.Name = name;

            Forecast forecast = api.getForecast(name, days);

            return View("Index", _service.GetForecastRedirect(forecast));
        }

        public ActionResult History()
        {
            ViewBag.Default = _service.CitiesByDefaultList();

            return View("CityHistory");
        }

        [HttpPost]
        public ActionResult History(CityHistoryViewModel city)
        {
            if (!ModelState.IsValid)
                return View("CityHistory", city);

            string name;

            if (city.CityName == null)
            {
                name = _service.GetCityNameById(city.CityByDefaultId);
            }
            else
            {
                name = city.CityName;
            }

            var cityInDb = _service.GetCityByName(name);

            if (cityInDb == null)
                return View();

            List<List> list = _service.GetListsWithTempsByCityid(cityInDb.Id);

            Forecast forecast = new Forecast();
            forecast.City = cityInDb;
            forecast.List = list;

            return View(forecast);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _service.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}