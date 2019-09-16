using BlazorPresentationDemo.Components;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorPresentationDemo.Services
{
    public class WeatherForecastService
    {
        private readonly string url = "";

        public WeatherForecastService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            this.httpClientFactory = httpClientFactory;
            Configuration = configuration;
        }

        private readonly IHttpClientFactory httpClientFactory;

        public IConfiguration Configuration { get; }

        public async Task<WeatherForecastItem[]> GetForecastAsync(string city, string countryCode)
        {
            var url = $"https://api.openweathermap.org/data/2.5/forecast?q={city},{countryCode}&mode=json&units=metric&APPID={Configuration["WeatherApiKey"]}";

            await Task.Delay(400);

            var resp = await httpClientFactory.CreateClient().GetStringAsync(url);

            var weatherResponse = JObject.Parse(resp);

            return weatherResponse.Value<JArray>("list")
                  .Select(s => new WeatherForecastItem
                  {
                      Time = DateTime.UnixEpoch.AddSeconds(s.Value<int>("dt")),
                      Temperature = s["main"].Value<double>("temp"),
                      Humidity = s["main"].Value<int>("humidity"),
                      Weather = s["weather"][0].Value<string>("main")
                  })
                  .OrderBy(o => o.Time)
                  .ToArray();
        }
    }
}
