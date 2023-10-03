using System.ComponentModel.DataAnnotations;

namespace WeatherApp.Models
{
    public class WeatherModel
    {
        public RequestModel WeatherRequestModel { get; set; }
        public List<ResponseModel> WeatherResponseModel { get; set; }
    }
}