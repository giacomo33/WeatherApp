using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WeatherApp.Helpers;

namespace WeatherApp.Converters
{
    public class StringToConditionImageConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value.ToString().ToLower())
            {
                case "broken clouds":
                    return "weathertype_large_partly_cloudy.png";

                case "clear sky":
                    return "weathertype_large_sunny.png";

                case "few clouds":
                    return "weathertype_large_few_clouds.png";

                case "light snow":
                    return "light_snow_shower_d.jpeg";

                case "overcast clouds":
                    return "weathertype_large_cloudy.png";

                case "scattered clouds":
                    return "weathertype_large_partly_cloudy.png";

                case "shower rain":
                    return "weathertype_large_rain_light.png";

                case "rain":
                    return "weathertype_large_rain.png";

                case "thunderstorm":
                    return "weathertype_large_thunderstorm.png";

                case "light rain":
                    return "weathertype_light_rain.png";

                case "moderate rain":
                    return "weathertype_moderate_rain.png";

                default:
                    return "weathertype_large_sunny.png";
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