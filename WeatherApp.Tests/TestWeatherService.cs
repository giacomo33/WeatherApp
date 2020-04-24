using AutoFixture;
using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using WeatherApp.Interfaces;
using WeatherApp.Models;
using WeatherApp.Services;
using Xamarin.Essentials;
using Xunit;

namespace WeatherApp.Tests
{
    public class TestWeatherService
    {
        private Mock<IWeatherService> mockedWeatherService;
        private Forecast forecast;
        private Location location = new Location(30.56, 60.99);

        public TestWeatherService()
        {
            mockedWeatherService = new Mock<IWeatherService>();

            forecast = new Fixture()
                .Create<Forecast>();

            mockedWeatherService
                .Setup((w) => w.GetWeatherForecast(location))
                .ReturnsAsync(forecast);
        }

        [Fact]
        public async Task WeatherService_return_correct_type()
        {
            //arange

            //act
            var result = await mockedWeatherService.Object.GetWeatherForecast(location);

            //assert
            result.Should().BeOfType<Forecast>();
        }

        [Fact]
        public async Task WeatherService_returns_7Days_forecast()
        {
            //arange
            // Setup sample forcast
            string json = File.ReadAllText(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), @"TestJson\sample1.json"));
            forecast = JsonConvert.DeserializeObject<Forecast>(json);
            mockedWeatherService
                .Setup((w) => w.GetWeatherForecast(location))
                .ReturnsAsync(forecast);

            //act
            var result = await mockedWeatherService.Object.GetWeatherForecast(location);

            //assert
            result.Daily.Count.Should().Be(8);
        }
    }
}