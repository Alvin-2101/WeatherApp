
using WeatherApp.Shared.Model;

namespace WeatherApp.Shared.Services
{
    public interface IWeatherService
    {
        Task<WeatherData?> GetWeatherAsync(string city);
    }
}