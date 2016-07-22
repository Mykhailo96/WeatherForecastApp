﻿using System.Collections.Generic;
using WeatherForecastApp.Models;

namespace WeatherForecastApp.Services
{
    public interface IHomeService
    {
        List<CityByDefault> CitiesByDefaultList();

        List<Days> DaysList();

        City GetCityById(int? id);

        City GetCityByName(string name);

        City AddCity(City city);

        List GetListByDateAndCityId(int dt, int cityId);

        Temp AddTemp(Temp temp);

        Temp GetTempById(int id);

        List AddList(List list);

        List<List> GetListsWithTempsByCityid(int id);

        string GetCityNameById(int id);

        int GetDaysNumberById(int id);

        Forecast GetForecast(Forecast forecast);

        Forecast GetForecastRedirect(Forecast forecast);

        void Dispose();
    }
}
