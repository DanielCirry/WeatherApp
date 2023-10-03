using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.DataLayer.Models
{
    public class WeatherResponse
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
    }
}
