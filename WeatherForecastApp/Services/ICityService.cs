using System.Collections.Generic;
using WeatherForecastApp.Models;

namespace WeatherForecastApp.Services
{
    public interface ICityService
    {
        List<CityByDefault> GetCityByDefaultList();

        void SaveAllChanges();

        void AddcityByDefault(CityByDefault city);

        CityByDefault GetCityByDefaultById(int? id);

        void RemoveCityByDefault(CityByDefault city);

        void Dispose();
    }
}
