using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using WeatherApp.Domain.Contracts;
using WeatherApp.Domain.Models;
using WeatherApp.Models;

namespace WeatherApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWeatherService _weatherService;

        public HomeController(ILogger<HomeController> logger, IWeatherService weatherService)
        {
            _logger = logger;
            _weatherService = weatherService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async Task<IActionResult> GetWeather(string location)
        {
            var response = new List<ResponseModel>();
            var sanitizedLocation = _weatherService.SanitizeLocation(location);
            var weatherRequestModel = new WeatherRequestModel()
            {
                Location = sanitizedLocation
            };
            
            var weather = await _weatherService.GetWeather(weatherRequestModel);

            if (weather.Count > 0)
            {
                var result = await _weatherService.SaveWeather(weather);
                if (result != null)
                {
                    foreach (var item in result)
                    {
                        response.Add(new ResponseModel()
                        {
                            Location = item.Location,
                            Temp = item.Temp,
                            Feels_Like = item.Feels_Like,
                            Sunset = item.Sunset,
                            Sunrise = item.Sunrise,
                            Cloud_Pct = item.Cloud_Pct,
                            Humidity = item.Humidity,
                            Wind_Speed = item.Wind_Speed,
                            Wind_Degrees = item.Wind_Degrees,
                            Max_Temp = item.Max_Temp,
                            Min_Temp = item.Min_Temp
                        });
                    }
                }
            }
           
            return PartialView("Partial/_GetWeatherPartial", response);
        }

        public IActionResult CleanRecords()
        {
            _weatherService.CleanRecords();

            return PartialView("Partial/_GetWeatherPartial", null);
        }
    }
}