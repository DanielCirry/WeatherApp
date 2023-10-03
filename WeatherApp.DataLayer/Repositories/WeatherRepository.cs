using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.DataLayer.Contracts;
using WeatherApp.DataLayer.Models;
using static System.Net.Mime.MediaTypeNames;

namespace WeatherApp.DataLayer.Repositories
{
    public class WeatherRepository : IWeatherRepository
    {
        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Databases", "WeatherDatabase.json");

        public async Task<List<WeatherResponse>> GetWeather(WeatherRequest weatherRequest)
        {
            var weatherResponse = new List<WeatherResponse>();

            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                var response = JsonConvert.DeserializeObject<List<WeatherResponse>>(json);
                if(response != null)
                {
                    weatherResponse.AddRange(response);
                }
            }

            if (weatherResponse == null)
            {
                return null;
            }
            
            return weatherResponse;
        }

        public async Task<List<WeatherResponse>> SaveWeather(List<WeatherResponse> weatherModel)
        {
            var weatherResponse = new List<WeatherResponse>();
            using (StreamReader r = new StreamReader(path))
            {
                string jsonRead = r.ReadToEnd();
                var response = JsonConvert.DeserializeObject<List<WeatherResponse>>(jsonRead);
                if (response?.Count > 0)
                {
                    weatherResponse.AddRange(response);
                }
            }

            var existingWeather = weatherResponse.IntersectBy(weatherModel.Select(x => x.Location), x => x.Location).ToList();
            if (!existingWeather.Any())
            {
                weatherResponse.AddRange(weatherModel);
            }

            string json = JsonConvert.SerializeObject(weatherResponse);

            using (var tw = new StreamWriter(path, false))
            {
                tw.WriteLine(json.ToString());
                tw.Close();
            }
            return weatherResponse;
        }

        public async void CleanRecords()
        {
            using (var tw = new StreamWriter(path, false))
            {
                tw.WriteLine();
                tw.Close();
            }
        }
    }
}
