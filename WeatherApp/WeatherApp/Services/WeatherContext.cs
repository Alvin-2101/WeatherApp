using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WeatherApp.Shared.Model;

namespace WeatherApp.Shared.Services
{
    public class WeatherContext : DbContext
    {
        public WeatherContext(DbContextOptions<WeatherContext> options) : base(options) { }

        public DbSet<WeatherRecord> WeatherRecords { get; set; }

        public void EnsureDatabaseCreated()
        {
            Database?.EnsureCreated();
        }
    }
}
