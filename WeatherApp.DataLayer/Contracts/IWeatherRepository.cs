using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.DataLayer.Models;

namespace WeatherApp.DataLayer.Contracts
{
    public interface IWeatherRepository
    {
        Task<List<WeatherResponse>> GetWeather(WeatherRequest weatherRequest);
        Task<List<WeatherResponse>> SaveWeather(List<WeatherResponse> weatherModel);
        void CleanRecords();
    }
}
