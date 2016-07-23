using System.Collections.Generic;
using System.Linq;
using WeatherForecastApp.Models;
using System.Data.Entity;
using System;
using System.Threading.Tasks;

namespace WeatherForecastApp.Services
{
    public class HomeService : IHomeService
    {
        private ApplicationDbContext _context;

        public HomeService()
        {
            _context = new ApplicationDbContext();
        }

        public async Task<List<CityByDefault>> CitiesByDefaultListAsync()
        {
            return await _context.CityByDefaults.ToListAsync();
        }

        public async Task<List<Days>> DaysListAsync()
        {
            return await _context.Days.ToListAsync();
        }

        public async Task<string> GetCityNameByIdAsync(int id)
        {
            var city = await _context.CityByDefaults.SingleOrDefaultAsync(c => c.Id == id);

            return city.Name;
        }

        public async Task<int> GetDaysNumberByIdAsync(int id)
        {
            var day = await _context.Days.SingleOrDefaultAsync(d => d.Id == id);

            return day.Number;
        }

        public async Task<City> GetCityByIdAsync(int? id)
        {
            return await _context.Cities.SingleOrDefaultAsync(c => c.Id == id);
        }

        public City AddCity(City city)
        {
            return _context.Cities.Add(city);
        }

        public Temp AddTemp(Temp temp)
        {
            return _context.Temps.Add(temp);
        }

        public List AddList(List list)
        {
            return _context.Lists.Add(list);
        }

        public async Task<List> GetListByDateAndCityIdAsync(int dt, int cityId)
        {
            return await _context.Lists.SingleOrDefaultAsync(d => d.Dt == dt && d.CityId == cityId);
        }

        public async Task<Temp> GetTempByIdAsync(int id)
        {
            return await _context.Temps.SingleOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Forecast> GetForecastAsync(Forecast forecast)
        {
            var cityInDb = await GetCityByIdAsync(forecast.City.Id);

            if (cityInDb == null)
            {
                cityInDb = AddCity(forecast.City);
            }

            await _context.SaveChangesAsync();

            int i = 0;
            foreach (var day in forecast.List)
            {
                string date = DateTime.Now.AddDays(i++).ToString("yyyy-MM-dd");

                var dayInDb = await GetListByDateAndCityIdAsync(day.Dt, forecast.City.Id);

                if (dayInDb == null)
                {
                    var newTemp = AddTemp(day.Temp);
                    newTemp.Date = date;

                    await _context.SaveChangesAsync();

                    day.TempId = newTemp.Id;
                    day.CityId = cityInDb.Id;

                    var newDay = AddList(day);

                    cityInDb.List.Add(newDay);

                    await _context.SaveChangesAsync();
                }
                else
                {   //update when forecast changes
                    dayInDb = day;

                    var newTemp = await GetTempByIdAsync(dayInDb.TempId);

                    newTemp = day.Temp;

                    await _context.SaveChangesAsync();
                }
            }

            return forecast;
        }

        public async Task<Forecast> GetForecastRedirectAsync(Forecast forecast)
        {
            var cityInDb = await GetCityByIdAsync(forecast.City.Id);

            int i = 0;

            foreach (var day in forecast.List)
            {
                string date = DateTime.Now.AddDays(i++).ToString("yyyy-MM-dd");

                var dayInDb = await GetListByDateAndCityIdAsync(day.Dt, forecast.City.Id);

                if (dayInDb == null)
                {
                    var newTemp = AddTemp(day.Temp);

                    newTemp.Date = date;
                    await _context.SaveChangesAsync();

                    day.TempId = newTemp.Id;
                    day.CityId = cityInDb.Id;

                    var newDay = AddList(day);

                    cityInDb.List.Add(newDay);

                    await _context.SaveChangesAsync();
                }
                else
                {
                    dayInDb = day;

                    var newTemp = await GetTempByIdAsync(dayInDb.TempId);

                    newTemp = day.Temp;

                    await _context.SaveChangesAsync();
                }
            }
            return forecast;
        }

        public async Task<City> GetCityByNameAsync(string name)
        {
            return await _context.Cities.SingleOrDefaultAsync(c => c.Name == name);
        }

        public async Task<List<List>> GetListsWithTempsByCityidAsync(int id)
        {
            return await _context.Lists.Include(t => t.Temp).Where(l => l.CityId == id).ToListAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}