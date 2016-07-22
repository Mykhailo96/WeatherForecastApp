using System.Collections.Generic;
using System.Linq;
using WeatherForecastApp.Models;
using WeatherForecastApp.ViewModels;
using System.Data.Entity;
using System;

namespace WeatherForecastApp.Services
{
    public class HomeService : IHomeService
    {
        private ApplicationDbContext _context;

        public HomeService()
        {
            _context = new ApplicationDbContext();
        }

        public City AddCity(City city)
        {
            return _context.Cities.Add(city);
        }

        public Temp AddTemp(Temp temp)
        {
            return _context.Temps.Add(temp);
        }

        public List<CityByDefault> CitiesByDefaultList()
        {
            return _context.CityByDefaults.ToList();
        }

        public List<Days> DaysList()
        {
            return _context.Days.ToList();
        }

        public City GetCityById(int? id)
        {
            return _context.Cities.SingleOrDefault(c => c.Id == id);
        }

        public string GetCityNameById(int id)
        {
            return _context.CityByDefaults.SingleOrDefault(c => c.Id == id).Name;
        }

        public List GetListByDateAndCityId(int dt, int cityId)
        {
            return _context.Lists.SingleOrDefault(d => d.Dt == dt && d.CityId == cityId);
        }

        public int GetDaysNumberById(int id)
        {
            return _context.Days.SingleOrDefault(d => d.Id == id).Number;
        }

        public List AddList(List list)
        {
            return _context.Lists.Add(list);
        }

        public Temp GetTempById(int id)
        {
            return _context.Temps.SingleOrDefault(t => t.Id == id);
        }

        public List<List> GetListsWithTempsByCityid(int id)
        {
            return _context.Lists.Include(t => t.Temp).Where(l => l.CityId == id).ToList();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public Forecast GetForecast(Forecast forecast)
        {
            var cityInDb = GetCityById(forecast.City.Id);

            if (cityInDb == null)
            {
                cityInDb = AddCity(forecast.City);
            }

            _context.SaveChanges();

            int i = 0;
            foreach (var day in forecast.List)
            {
                string date = DateTime.Now.AddDays(i++).ToString("yyyy-MM-dd");

                var dayInDb = GetListByDateAndCityId(day.Dt, forecast.City.Id);

                if (dayInDb == null)
                {
                    var newTemp = AddTemp(day.Temp);
                    newTemp.Date = date;

                    _context.SaveChanges();

                    day.TempId = newTemp.Id;
                    day.CityId = cityInDb.Id;

                    var newDay = AddList(day);

                    cityInDb.List.Add(newDay);

                    _context.SaveChanges();
                }
                else
                {   //update when forecast changes
                    dayInDb = day;

                    var newTemp = GetTempById(dayInDb.TempId);

                    newTemp = day.Temp;

                    _context.SaveChanges();
                }
            }

            return forecast;
        }

        public Forecast GetForecastRedirect(Forecast forecast)
        {
            var cityInDb = GetCityById(forecast.City.Id);

            int i = 0;

            foreach (var day in forecast.List)
            {
                string date = DateTime.Now.AddDays(i++).ToString("yyyy-MM-dd");

                var dayInDb = GetListByDateAndCityId(day.Dt, forecast.City.Id);

                if (dayInDb == null)
                {
                    var newTemp = AddTemp(day.Temp);

                    newTemp.Date = date;
                    _context.SaveChanges();

                    day.TempId = newTemp.Id;
                    day.CityId = cityInDb.Id;

                    var newDay = AddList(day);

                    cityInDb.List.Add(newDay);

                    _context.SaveChanges();
                }
                else
                {
                    dayInDb = day;

                    var newTemp = GetTempById(dayInDb.TempId);

                    newTemp = day.Temp;

                    _context.SaveChanges();
                }
            }
            return forecast;
        }

        public City GetCityByName(string name)
        {
            return _context.Cities.SingleOrDefault(c => c.Name == name);
        }
    }
}