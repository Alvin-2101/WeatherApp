using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WeatherApp.Shared.Model;
using WeatherApp.Shared.Services;

namespace WeatherApp.Services
{
    public class WeatherDataService : IWeatherDataService
    {
        private readonly WeatherContext _context;

        public WeatherDataService(WeatherContext context)
        {
            _context = context;
        }

        public async Task<List<WeatherRecord>> GetWeatherRecordsAsync()
        {
            return await _context.WeatherRecords.ToListAsync();
        }

        public async Task<WeatherRecord?> GetWeatherRecordAsync(Guid id)
        {
            return await _context.WeatherRecords.FindAsync(id);
        }

        public async Task AddWeatherRecordAsync(WeatherRecord weatherRecord)
        {
            _context.WeatherRecords.Add(weatherRecord);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateWeatherRecordAsync(WeatherRecord weatherRecord)
        {
            _context.WeatherRecords.Update(weatherRecord);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteWeatherRecordAsync(Guid id)
        {
            var weatherRecord = await _context.WeatherRecords.FindAsync(id);
            if (weatherRecord != null)
            {
                _context.WeatherRecords.Remove(weatherRecord);
                await _context.SaveChangesAsync();
            }
        }
    }
}
