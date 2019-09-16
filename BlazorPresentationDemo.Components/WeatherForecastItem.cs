using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorPresentationDemo.Components
{
    public class WeatherForecastItem
    {
        public DateTime Time { get; set; }
        public double Temperature { get; set; }
        public int Humidity { get; set; }
        public string Weather { get; set; }

    }
}
