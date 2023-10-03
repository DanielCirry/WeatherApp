using System.ComponentModel.DataAnnotations;

namespace WeatherApp.Models
{
    public class RequestModel
    {
        [Required]
        public string Location { get; set; }
    }
}