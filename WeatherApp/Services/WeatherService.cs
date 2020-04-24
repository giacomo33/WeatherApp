using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Interfaces;
using WeatherApp.Models;
using Xamarin.Essentials;

namespace WeatherApp.Services
{
    /// <summary>Weather service implementation using the openweathermap api.</summary>
    public class WeatherService : IWeatherService
    {
        private const string OpenWeatherMapUri = "https://api.openweathermap.org/data/2.5/onecall?lat={0}&lon={1}&units=metric&appid=ef8b4e325067976d41a04294f5d7fefa";

        public async Task<Forecast> GetWeatherForecast(Location location)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var url = string.Format(OpenWeatherMapUri, location.Latitude, location.Longitude);
                    var response = await httpClient.GetAsync(url);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<Forecast>(jsonResponse);
                    }
                    return null;
                }
            }
            catch (HttpRequestException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
            }
        }
    }
}