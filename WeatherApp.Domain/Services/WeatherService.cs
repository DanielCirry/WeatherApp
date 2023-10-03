using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WeatherApp.DataLayer.Contracts;
using WeatherApp.DataLayer.Models;
using WeatherApp.Domain.Contracts;
using WeatherApp.Domain.Models;

namespace WeatherApp.Domain.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IWeatherRepository _weatherRepository;

        public WeatherService(IWeatherRepository weatherRepository)
        {
            _weatherRepository = weatherRepository;
        }
        public async Task<List<WeatherResponseModel>> GetWeather(WeatherRequestModel weatherRequest)
        {
            var weatherResponse = new List<WeatherResponseModel>();
            var request = new WeatherRequest()
            {
                Location = weatherRequest.Location
            };
            var data = await _weatherRepository.GetWeather(request);

            if (data != null)
            {
                if (string.IsNullOrWhiteSpace(weatherRequest.Location))
                {
                    weatherResponse = CreateWeatherResponseModelList(data);

                    return weatherResponse;
                }

                var savedLocation = data.FirstOrDefault(x => x.Location.Equals(weatherRequest.Location));
                if (savedLocation != null)
                {
                    weatherResponse.Add(CreateWeatherResponseModel(savedLocation));
                }
                else
                {
                    await GetWeatherFromApi(weatherRequest, weatherResponse);
                }
            }

            return weatherResponse;
        }

        public async Task<List<WeatherResponseModel>> SaveWeather(List<WeatherResponseModel> weatherResponse)
        {
            var weatherList = new List<WeatherResponse>();

            foreach (var weather in weatherResponse)
            {
                weatherList.Add(CreateWeatherResponse(weather));
            }

            var data = await _weatherRepository.SaveWeather(weatherList);

            if (data == null)
                return null;

            var result = CreateWeatherResponseModelList(data);

            return result;
        }

        public async void CleanRecords()
        {
            _weatherRepository.CleanRecords();
        }

        public string SanitizeLocation(string location)
        {
            if (string.IsNullOrWhiteSpace(location))
                return string.Empty;

            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            string cleanedLocation = textInfo.ToTitleCase(location);

            return cleanedLocation.Trim();
        }
        private async Task GetWeatherFromApi(WeatherRequestModel weatherRequest, List<WeatherResponseModel> weatherResponse)
        {
            //This could be in settings
            var apiKey = Environment.GetEnvironmentVariable("API_KEY", EnvironmentVariableTarget.Process);
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("X-Api-Key", apiKey);
                using (var response = await httpClient.GetAsync($"https://api.api-ninjas.com/v1/weather?city={weatherRequest.Location}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var weather = JsonConvert.DeserializeObject<WeatherResponseModel>(apiResponse);
                        if (weather != null)
                        {
                            weatherResponse.Add(CreateWeatherResponseWithLocation(weather, weatherRequest.Location));
                        }
                    }
                }
            }
        }

        private List<WeatherResponseModel> CreateWeatherResponseModelList(List<WeatherResponse> model)
        {
            var result = new List<WeatherResponseModel>();

            foreach (var weather in model)
            {
                result.Add(new WeatherResponseModel()
                {
                    Location = weather.Location,
                    Cloud_Pct = weather.Cloud_Pct,
                    Feels_Like = weather.Feels_Like,
                    Humidity = weather.Humidity,
                    Max_Temp = weather.Max_Temp,
                    Min_Temp = weather.Min_Temp,
                    Sunrise = weather.Sunrise,
                    Sunset = weather.Sunset,
                    Temp = weather.Temp,
                    Wind_Degrees = weather.Wind_Degrees,
                    Wind_Speed = weather.Wind_Speed
                });
            };

            return result;
        }

        private WeatherResponseModel CreateWeatherResponseModel(WeatherResponse weather)
        {
                return new WeatherResponseModel()
                {
                    Location = weather.Location,
                    Cloud_Pct = weather.Cloud_Pct,
                    Feels_Like = weather.Feels_Like,
                    Humidity = weather.Humidity,
                    Max_Temp = weather.Max_Temp,
                    Min_Temp = weather.Min_Temp,
                    Sunrise = weather.Sunrise,
                    Sunset = weather.Sunset,
                    Temp = weather.Temp,
                    Wind_Degrees = weather.Wind_Degrees,
                    Wind_Speed = weather.Wind_Speed
                };
        }

        private WeatherResponse CreateWeatherResponse(WeatherResponseModel model)
        {
            return new WeatherResponse()
            {
                Location = model.Location,
                Cloud_Pct = model.Cloud_Pct,
                Feels_Like = model.Feels_Like,
                Humidity = model.Humidity,
                Max_Temp = model.Max_Temp,
                Min_Temp = model.Min_Temp,
                Sunrise = model.Sunrise,
                Sunset = model.Sunset,
                Temp = model.Temp,
                Wind_Degrees = model.Wind_Degrees,
                Wind_Speed = model.Wind_Speed
            };
        }

        private WeatherResponseModel CreateWeatherResponseWithLocation(WeatherResponseModel model, string location)
        {
            return new WeatherResponseModel()
            {
                Location = location,
                Cloud_Pct = model.Cloud_Pct,
                Feels_Like = model.Feels_Like,
                Humidity = model.Humidity,
                Max_Temp = model.Max_Temp,
                Min_Temp = model.Min_Temp,
                Sunrise = model.Sunrise,
                Sunset = model.Sunset,
                Temp = model.Temp,
                Wind_Degrees = model.Wind_Degrees,
                Wind_Speed = model.Wind_Speed
            };
        }
    }
}
