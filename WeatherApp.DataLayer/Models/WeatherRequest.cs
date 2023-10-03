using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.DataLayer.Models
{
    public class WeatherRequest
    {
        public string Location { get; set; }
    }
}
