using BlazorPresentationDemo.Shared.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorPresentationDemo.Shared.Services
{
    public class WeatherForecastService
    {
        public WeatherForecastService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        private readonly HttpClient httpClient;

        public async Task<WeatherForecastItem[]> GetForecastAsync(string city, string countryCode, string apiKey)
        {
            var url = $"https://api.openweathermap.org/data/2.5/forecast?q={city},{countryCode}&mode=json&units=metric&APPID={apiKey}";

            await Task.Delay(400);

            var resp = await httpClient.GetStringAsync(url);

            var weatherResponse = JObject.Parse(resp);

            return weatherResponse.Value<JArray>("list")
                  .Select(s => new WeatherForecastItem
                  {
                      Time = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(s.Value<int>("dt")),
                      Temperature = s["main"].Value<double>("temp"),
                      Humidity = s["main"].Value<int>("humidity"),
                      Weather = s["weather"][0].Value<string>("main")
                  })
                  .OrderBy(o => o.Time)
                  .ToArray();
        }
    }
}
