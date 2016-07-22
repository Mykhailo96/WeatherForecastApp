using System.Collections.Generic;
using System.Linq;
using WeatherForecastApp.Models;

namespace WeatherForecastApp.Services
{
    public class CityService : ICityService
    {
        private ApplicationDbContext _context;

        public CityService()
        {
            _context = new ApplicationDbContext();
        }

        public List<CityByDefault> GetCityByDefaultList()
        {
            return _context.CityByDefaults.ToList();
        }

        public void SaveAllChanges()
        {
            _context.SaveChanges();
        }

        public void AddcityByDefault(CityByDefault city)
        {
            _context.CityByDefaults.Add(city);
        }

        public CityByDefault GetCityByDefaultById(int? id)
        {
            return _context.CityByDefaults.SingleOrDefault(c => c.Id == id);
        }

        public void RemoveCityByDefault(CityByDefault city)
        {
            _context.CityByDefaults.Remove(city);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}