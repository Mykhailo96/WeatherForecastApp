using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherForecastApp.Models;

namespace WeatherForecastApp.Services
{
    public interface IHomeService
    {
        Task<List<CityByDefault>> CitiesByDefaultListAsync();

        Task<List<Days>> DaysListAsync();

        Task<string> GetCityNameByIdAsync(int id);

        Task<int> GetDaysNumberByIdAsync(int id);

        Task<Forecast> GetForecastAsync(Forecast forecast);

        Task<City> GetCityByIdAsync(int? id);

        City AddCity(City city);

        Temp AddTemp(Temp temp);

        List AddList(List list);

        Task<List> GetListByDateAndCityIdAsync(int dt, int cityId);

        Task<Temp> GetTempByIdAsync(int id);

        Task<Forecast> GetForecastRedirectAsync(Forecast forecast);

        Task<City> GetCityByNameAsync(string name);

        Task<List<List>> GetListsWithTempsByCityidAsync(int id);

        void Dispose();
    }
}
