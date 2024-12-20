using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Shared.Model;

namespace WeatherApp.Shared.Services
{
    public interface IWeatherDataService
    {
        Task<List<WeatherRecord>> GetWeatherRecordsAsync();
        Task<WeatherRecord?> GetWeatherRecordAsync(Guid id);
        Task AddWeatherRecordAsync(WeatherRecord weatherRecord);
        Task UpdateWeatherRecordAsync(WeatherRecord weatherRecord);
        Task DeleteWeatherRecordAsync(Guid id);
    }
}
