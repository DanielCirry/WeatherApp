using System.ComponentModel.DataAnnotations;

namespace WeatherApp.Models
{
    public class ResponseModel
    {
        public string Location { get; set; }
        public string Cloud_Pct { get; set; }
        public string Temp { get; set; }
        public string Feels_Like { get; set; }
        public string Humidity { get; set; }
        public string Min_Temp { get; set; }
        public string Max_Temp { get; set; }
        public string Wind_Speed { get; set; }
        public string Wind_Degrees { get; set; }
        public string Sunrise { get; set; }
        public string Sunset { get; set; }
        public TimeSpan SunriseDateTime => ConvertToDateTime(Sunrise);
        public TimeSpan SunsetDateTime => ConvertToDateTime(Sunset);

        public TimeSpan ConvertToDateTime(string time)
        {
            return DateTimeOffset.FromUnixTimeSeconds(long.Parse(time)).TimeOfDay;
        }
    }
}