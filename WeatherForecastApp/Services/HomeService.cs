using System.Collections.Generic;
using System.Linq;
using WeatherForecastApp.Models;
using System.Data.Entity;

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

        public void SaveAllChanges()
        {
            _context.SaveChanges();
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
    }
}