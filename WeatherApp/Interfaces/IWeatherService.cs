using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;
using Xamarin.Essentials;

namespace WeatherApp.Interfaces
{
    /// <summary>Interface for weather forecast service</summary>
    public interface IWeatherService
    {
        Task<Forecast> GetWeatherForecast(Location location);
    }
}