using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.ViewModel;
using System.ComponentModel;
using System.Windows;

namespace TestApp.Model
{
    class Weather : NotificationObject
    {
        private decimal temperature;
        private decimal windSpeed;
        private string windDirection;
        private string clouds;

        public decimal Temperature
        {
            get { return temperature; }
            set {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    temperature = value;
                    RaisePropertyChanged("Temperature");
                });
            }
        }

        public decimal WindSpeed
        {
            get { return windSpeed; }
            set
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    windSpeed = value;
                    RaisePropertyChanged("WindSpeed");
                });
            }
        }

        public string WindDirection
        {
            get { return windDirection; }
            set
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    windDirection = value;
                    RaisePropertyChanged("WindDirection");
                });
            }
        }
        
        public string Clouds
        {
            get { return clouds; }
            set
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    clouds = value;
                    RaisePropertyChanged("Clouds");
                });
            }
        }
    }
}
