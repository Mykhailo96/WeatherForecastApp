using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherForecastApp.Models;

namespace WeatherForecastApp.Services
{
    public interface ICityService
    {
        Task<List<CityByDefault>> GetCityByDefaultListAsync();

        Task SaveAllChangesAsync();

        void AddcityByDefault(CityByDefault city);

        Task<CityByDefault> GetCityByDefaultByIdAsync(int? id);

        void RemoveCityByDefault(CityByDefault city);

        void Dispose();
    }
}
