using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Domain.Models;

namespace WeatherApp.Domain.Contracts
{
    public interface IWeatherService
    {
        Task<List<WeatherResponseModel>> GetWeather(WeatherRequestModel weatherModel);
        Task<List<WeatherResponseModel>> SaveWeather(List<WeatherResponseModel> weatherModel);
        void CleanRecords();
        string SanitizeLocation(string location);
    }
}
