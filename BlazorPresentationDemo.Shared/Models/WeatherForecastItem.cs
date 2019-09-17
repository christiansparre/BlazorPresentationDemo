using System;

namespace BlazorPresentationDemo.Shared.Models
{
    public class WeatherForecastItem
    {
        public DateTime Time { get; set; }
        public double Temperature { get; set; }
        public int Humidity { get; set; }
        public string Weather { get; set; }

    }
}
