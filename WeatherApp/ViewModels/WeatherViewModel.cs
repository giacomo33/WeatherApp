using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeZoneConverter;
using WeatherApp.Helpers;
using WeatherApp.Interfaces;
using WeatherApp.Models;
using WeatherApp.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WeatherApp.ViewModels
{
    /// <summary>View Model implementation for the WeatherPage</summary>
    public class WeatherViewModel : BaseViewModel
    {
        public IWeatherService WeatherService => DependencyService.Get<IWeatherService>();

        public ICommand ViewDayWeatherTapCommand { get; }
        public ICommand RefreshCommand { get; }
        protected bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

        private string city;

        public string City
        {
            get { return city; }
            set
            {
                SetProperty(ref city, value);
            }
        }

        private bool isToday = true;

        public bool IsToday
        {
            get { return isToday; }
            set
            {
                SetProperty(ref isToday, value);
            }
        }

        private Color selectedColor;

        public Color SelectedColor
        {
            get { return selectedColor; }
            set
            {
                SetProperty(ref selectedColor, value);
            }
        }

        private double currenTemperature;

        public double CurrenTemperature
        {
            get { return currenTemperature; }
            set
            {
                SetProperty(ref currenTemperature, value);
            }
        }

        private DayWeather selectedDayWeather;

        public DayWeather SelectedDayWeather
        {
            get { return selectedDayWeather; }
            set
            {
                SetProperty(ref selectedDayWeather, value);
            }
        }

        private ObservableCollection<DayWeather> sevenDayForecast;

        public ObservableCollection<DayWeather> SevenDayForecast
        {
            get { return sevenDayForecast; }
            set
            {
                SetProperty(ref sevenDayForecast, value);
            }
        }

        public WeatherViewModel()
        {
            Title = "Today's Forecast";

            // Setup commands
            ViewDayWeatherTapCommand = new Command<DayWeather>((d) => SetDayWeather(d));
            RefreshCommand = new Command(async () => await ExecuteGetCurrentWeatherAsync());

            ExecuteGetCurrentWeatherAsync().GetAwaiter();
        }

        private async Task ExecuteGetCurrentWeatherAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                // Obtain the device location
                var location = await DeviceLocation.GetLocationAsync();
                if (location == null)
                {
                    DisplayLocationPermissionAlert();
                    return;
                }

                // Obtain the forecast from the weather service
                var forecast = await WeatherService.GetWeatherForecast(location);
                if (forecast == null)
                {
                    DisplayWebAPIAlert();
                    return;
                }

                // Obtain the timeazone of the location...used for the datetime calculations
                TimeZoneInfo tzi = TZConvert.GetTimeZoneInfo(forecast.Timezone);
                City = GetCity(forecast.Timezone);

                CurrenTemperature = forecast.Current.Temp;

                //Setup 7 Day Forecast
                SevenDayForecast = new ObservableCollection<DayWeather>();
                foreach (var day in forecast.Daily)
                {
                    SevenDayForecast.Add(new DayWeather
                    {
                        Day = GetDayOfWeek(tzi, day.Dt),
                        Mintemperature = day.Temp.Min,
                        Maxtemperature = day.Temp.Max,
                        Description = day.Weather[0].Description.First().ToString().ToUpper() + day.Weather[0].Description.Substring(1),
                        Sunrise = GetTimeString(tzi, day.Sunrise),
                        Sunset = GetTimeString(tzi, day.Sunset),
                        DayColor = Color.DarkGray
                    });
                }

                SetDayWeather(sevenDayForecast[0]);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private static void DisplayWebAPIAlert()
        {
            var title = "Weather API Issue";
            var message = "Please check your OpenWeatherMap settings.";
            Application.Current?.MainPage?.DisplayAlert(title, message, "Ok");
        }

        private static void DisplayLocationPermissionAlert()
        {
            var title = "Location Permission";
            var message = "To get the weather for your current city the location permission is required. Please go into Settings and turn on Location for the app.";
            Application.Current?.MainPage?.DisplayAlert(title, message, "Ok");
        }

        /// <summary>Converts the Unix timestamp to datetime.</summary>
        /// <param name="unixTimeStamp">The unix time stamp.</param>
        /// <returns></returns>
        private DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp);
            return dtDateTime;
        }

        private string GetCity(string timezone)
        {
            return timezone.Split(new Char[] { '/' })[1].Replace("_", " ");
        }

        private string GetTimeString(TimeZoneInfo timezone, long unixTimeUTC)
        {
            var dateTime = UnixTimeStampToDateTime(unixTimeUTC);
            DateTimeOffset localTime = TimeZoneInfo.ConvertTimeFromUtc(dateTime, timezone);

            return localTime.ToString("h:mm tt");
        }

        /// <summary>Gets the day of week based on timezone and unix time.</summary>
        /// <param name="timezone">The timezone.</param>
        /// <param name="unixTimeUTC">The unix time UTC.</param>
        /// <returns></returns>
        private string GetDayOfWeek(TimeZoneInfo timezone, long unixTimeUTC)
        {
            var dateTime = UnixTimeStampToDateTime(unixTimeUTC);
            DateTimeOffset localTime = TimeZoneInfo.ConvertTimeFromUtc(dateTime, timezone);

            if (localTime.Date == TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timezone).Date)
            {
                IsToday = true;
                return "Today";
            }
            else
            {
                IsToday = false;
                return localTime.DayOfWeek.ToString();
            }
        }

        /// <summary>Sets the day weather for the currently selected day.</summary>
        /// <param name="dayWeather">The day weather.</param>
        private void SetDayWeather(DayWeather dayWeather)
        {
            IsToday = dayWeather.Day.ToString() == "Today" ? true : false;
            foreach (var item in SevenDayForecast)
            {
                if (item.Day == dayWeather.Day)
                {
                    item.DayColor = Color.GreenYellow;
                    item.IsSelected = true;
                }
                else
                {
                    item.DayColor = Color.DarkGray;
                    item.IsSelected = false;
                }
            }
            SelectedDayWeather = dayWeather;
        }
    }
}