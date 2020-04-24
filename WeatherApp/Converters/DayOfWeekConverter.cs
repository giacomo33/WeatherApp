using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WeatherApp.Helpers;

namespace WeatherApp.Converters
{
    public class DayOfWeekConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value.ToString())
            {
                case "Monday":
                    return "Mon";

                case "Tuesday":
                    return "Tue";

                case "Wednesday":
                    return "Wed";

                case "Thursday":
                    return "Thu";

                case "Friday":
                    return "Fri";

                case "Saturday":
                    return "Sat";

                case "Sunday":
                    return "Sun";

                default:
                    return "Today";
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