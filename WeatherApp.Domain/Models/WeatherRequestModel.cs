using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Domain.Models
{
    public class WeatherRequestModel
    {
        public string Location { get; set; }
    }
}
