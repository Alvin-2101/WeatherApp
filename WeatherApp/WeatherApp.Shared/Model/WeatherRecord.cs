using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WeatherApp.Shared.Model
{
    public class WeatherRecord
    {
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
        public string Icon { get; set; }
        public double Temperature { get; set; }
        public string ShortForecast { get; set; }
    }
}
