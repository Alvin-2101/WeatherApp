using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WeatherApp.Shared.Model;
using WeatherApp.Shared.Services;

namespace WeatherApp.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;

        public WeatherService()
        {
            _httpClient = new HttpClient();
        }

        private async Task<GeoLocationData?> GetGeoLocationDataAsync(string city)
        {
            try
            {
                
                var url = $"https://geocode.maps.co/search?q={city}&api_key=676490d00cb72289834378arfe2e10a";
                var results = await _httpClient.GetAsync(url);

                if (results == null || !results.IsSuccessStatusCode)
                    return null;

                var json = await results.Content.ReadAsStringAsync();
                var jsonArray = JArray.Parse(json);
                if (jsonArray.Count < 1)
                    return null;
                var geoLocationData = new GeoLocationData()
                { 
                    Lat = Math.Round(double.Parse(jsonArray[0]["lat"].ToString()), 4) ,
                    Lon = Math.Round(double.Parse(jsonArray[0]["lon"].ToString()), 4) 
                };
                return geoLocationData;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<WeatherData?> GetWeatherAsync(string city)
        {
            try
            {
                var geoLocationData = await GetGeoLocationDataAsync(city);

                if (geoLocationData == null)
                    return null;
                //https://api.weather.gov/points/38.8894,-77.0352
                var url = $"https://api.weather.gov/points/{geoLocationData.Lat},{geoLocationData.Lon}";
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("User-Agent", "WeatherApp/1.0 (alfernandes2101@gmail.com)");

                var results = await _httpClient.SendAsync(request);
               
                if (results == null || !results.IsSuccessStatusCode)
                    return null;

                var json = await results.Content.ReadAsStringAsync();
                var jObject = JObject.Parse(json);
                var forecastUrl = jObject["properties"]["forecast"].ToString();
                request = new HttpRequestMessage(HttpMethod.Get, forecastUrl);
                request.Headers.Add("User-Agent", "WeatherApp/1.0 (alfernandes2101@gmail.com)");
                results = await _httpClient.SendAsync(request);

                if (results == null || !results.IsSuccessStatusCode)
                    return null;

                 json = await results.Content.ReadAsStringAsync();
                 jObject = JObject.Parse(json);
                var periods = jObject["properties"]["periods"].ToString();
                var weatherData = new WeatherData
                {
                    Periods = JsonConvert.DeserializeObject<List<Period>>(periods) ?? new List<Period>()
                };
                return weatherData;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
