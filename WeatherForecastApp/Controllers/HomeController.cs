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

            var cityInDb = _service.GetCityById(forecast.City.Id);

            if (cityInDb == null)
            {
                cityInDb = _service.AddCity(forecast.City);
            }

            _service.SaveAllChanges();

            int i = 0;
            foreach (var day in forecast.List)
            {
                string date = DateTime.Now.AddDays(i++).ToString("yyyy-MM-dd");

                var dayInDb = _service.GetListByDateAndCityId(day.Dt, forecast.City.Id);

                if (dayInDb == null)
                {
                    var newTemp = _service.AddTemp(day.Temp);
                    newTemp.Date = date;

                    _service.SaveAllChanges();

                    day.TempId = newTemp.Id;
                    day.CityId = cityInDb.Id;

                    var newDay = _service.AddList(day);

                    cityInDb.List.Add(newDay);

                    _service.SaveAllChanges();
                }
                else
                {   //update when forecast changes
                    dayInDb = day;

                    var newTemp = _service.GetTempById(dayInDb.TempId);

                    newTemp = day.Temp;

                    _service.SaveAllChanges();
                }
            }

            return View(forecast);
        }

        public ActionResult Redirect(string name, int days)
        {
            ViewBag.Name = name;

            Forecast forecast = api.getForecast(name, days);

            var cityInDb = _service.GetCityById(forecast.City.Id);
            
            int i = 0;

            foreach (var day in forecast.List)
            {
                string date = DateTime.Now.AddDays(i++).ToString("yyyy-MM-dd");

                var dayInDb = _service.GetListByDateAndCityId(day.Dt, forecast.City.Id);

                if (dayInDb == null)
                {
                    var newTemp = _service.AddTemp(day.Temp);

                    newTemp.Date = date;
                    _service.SaveAllChanges();

                    day.TempId = newTemp.Id;
                    day.CityId = cityInDb.Id;

                    var newDay = _service.AddList(day);

                    cityInDb.List.Add(newDay);

                    _service.SaveAllChanges();
                }
                else
                {
                    dayInDb = day;

                    var newTemp = _service.GetTempById(dayInDb.TempId);

                    newTemp = day.Temp;

                    _service.SaveAllChanges();
                }  
            }           

            return View("Index", forecast);
        }

        public ActionResult History(int? id)
        {
            var city = _service.GetCityById(id);

            if(city == null)
                return HttpNotFound();

            List<List> list = _service.GetListsWithTempsByCityid(city.Id);

            Forecast forecast = new Forecast();
            forecast.City = city;
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