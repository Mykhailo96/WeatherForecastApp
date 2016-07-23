using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeatherForecastApp.Models;
using WeatherForecastApp.ViewModels;
using System.Data.Entity;
using WeatherForecastApp.Services;
using System.Threading.Tasks;

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

        public async Task<ActionResult> City()
        {
            ViewBag.Default = await _service.CitiesByDefaultListAsync();
            ViewBag.Days = await _service.DaysListAsync();

            return View();
        }

        public async Task<ActionResult> Index(CityViewModel city)
         {
            if (!ModelState.IsValid)
                return View("City", city);

            string name;

            if(city.CityName == null)
            {
                name = await _service.GetCityNameByIdAsync(city.CityByDefaultId);
            }
            else
            {
                name = city.CityName;
            }

            ViewBag.Name = name;

            int days = await _service.GetDaysNumberByIdAsync(city.DaysId);

            Forecast forecast = await api.GetForecastAsync(name, days);

            return View(await _service.GetForecastAsync(forecast));
        }

        public async Task<ActionResult> Redirect(string name, int days)
        {
            ViewBag.Name = name;

            Forecast forecast = await api.GetForecastAsync(name, days);

            return View("Index", await _service.GetForecastRedirectAsync(forecast));
        }

        public async Task<ActionResult> History()
        {
            ViewBag.Default = await _service.CitiesByDefaultListAsync();

            return View("CityHistory");
        }

        [HttpPost]
        public async Task<ActionResult> History(CityHistoryViewModel city)
        {
            if (!ModelState.IsValid)
                return View("CityHistory", city);

            string name;

            if (city.CityName == null)
            {
                name = await _service.GetCityNameByIdAsync(city.CityByDefaultId);
            }
            else
            {
                name = city.CityName;
            }

            var cityInDb = await _service.GetCityByNameAsync(name);

            if (cityInDb == null)
                return View();

            List<List> list = await _service.GetListsWithTempsByCityidAsync(cityInDb.Id);

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