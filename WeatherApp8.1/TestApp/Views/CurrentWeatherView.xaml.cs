using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using TestApp.ViewModels;
using System.Windows.Data;
using System.Device;
using System.Device.Location;

namespace TestApp.Views
{
    public partial class CurrentWeatherView : UserControl
    {
        public CurrentWeatherView()
        {
            InitializeComponent();
            
            this.Loaded += CurrentWeatherView_Loaded;
        }

        void CurrentWeatherView_Loaded(object sender, RoutedEventArgs e)
        {
            
            DataContext = new CurrentWeather(LoadLocation());
            ((CurrentWeather)DataContext).GetWeather();
        }

        public string LoadLocation()
        {
            return "98110";
        }
    }
}
