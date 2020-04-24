using MvvmHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using Xamarin.Forms;

namespace WeatherApp.Models
{
    /// <summary>DayWeather model</summary>
    public class DayWeather : ObservableObject
    {
        public string Day { get; set; } = string.Empty;

        public double Maxtemperature { get; set; } = 0;

        public double Mintemperature { get; set; } = 0;

        public string Description { get; set; } = string.Empty;

        public string Sunrise { get; set; } = string.Empty;

        public string Sunset { get; set; } = string.Empty;

        private Color dayColor;

        public Color DayColor
        {
            get { return dayColor; }
            set
            {
                dayColor = value;
                OnPropertyChanged("DayColor");
            }
        }

        private bool isSelected;

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }
    }
}