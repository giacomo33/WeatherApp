using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WeatherApp.Helpers;

namespace WeatherApp.Converters
{
    public class StringToBackgroundImageConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value.ToString().ToLower())
            {
                case "broken clouds":
                    return "partly_cloudy_n.png";

                case "clear sky":
                    return "sunny_d.png";

                case "few clouds":
                    return "sunny_intervals_d.png";

                case "light snow":
                    return "light_snow_shower_d.png";

                case "overcast clouds":
                    return "white_cloud_d.png";

                case "scattered clouds":
                    return "partly_cloudy_n.png";

                case "shower rain":
                    return "light_rain_shower_d.png";

                case "rain":
                    return "heavy_rain_d.png";

                case "light rain":
                    return "light_rain_d.png";

                case "moderate rain":
                    return "light_rain_d.png";

                case "thunderstorm":
                    return "thunderstorm_d.png";

                default:
                    return "sunny_d.png";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}