using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
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

        public async Task<List<CityByDefault>> GetCityByDefaultListAsync()
        {
            return await _context.CityByDefaults.ToListAsync();
        }

        public async Task SaveAllChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void AddcityByDefault(CityByDefault city)
        {
            _context.CityByDefaults.Add(city);
        }

        public async Task<CityByDefault> GetCityByDefaultByIdAsync(int? id)
        {
            return await _context.CityByDefaults.SingleOrDefaultAsync(c => c.Id == id);
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