using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WeatherApp.ViewModel;
using Microsoft.Devices;
using Microsoft.Phone;
using System.Threading;

namespace WeatherApp.Views
{
    public partial class CurrentWeatherView : UserControl
    {

        public CurrentWeatherView()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(CurrentWeatherView_Loaded);
        }

        void CurrentWeatherView_Loaded(object sender, RoutedEventArgs e)
        {

            DataContext = new CurrentWeather("97220");
            ((CurrentWeather)DataContext).GetWeather();
        }
    }
}
