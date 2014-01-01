using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Practices.Prism.ViewModel;
using System.Net;
using WeatherApp.Model;
using System.IO;
using System.Xml;
using System.Windows;
using System.ComponentModel;
using System.Device.Location;

namespace WeatherApp.ViewModel
{
    public class CurrentWeather : NotificationObject
    {
        private string currentLocation;
        private WebRequest request;
        private Weather immediateWeather = new Weather();

        public CurrentWeather(double lat, double lon)
        {
            currentLocation = lat.ToString() + "," + lon.ToString();
            Init();
        }

        public CurrentWeather(string location)
        {
            currentLocation = location;
            Init();
        }

        public void Init()
        {
            request = WebRequest.Create(string.Format("http://api.wunderground.com/api/4c1a2dce443aa7ce/conditions/q/{0}.xml", currentLocation));
            immediateWeather.PropertyChanged += immediateWeather_PropertyChanged;
        }

        public Weather ImmediateWeather
        {
            get { return immediateWeather; }
            set { }
        }

        void immediateWeather_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(e.PropertyName);
        }

        public void GetWeather()
        {  
            request.BeginGetResponse(new AsyncCallback(SetWeather), null);
        }

        public void SetWeather(IAsyncResult result)
        {
            WebResponse response = request.EndGetResponse(result);
            Stream s = response.GetResponseStream();
            StringBuilder responseString = new StringBuilder();
            XmlReader xmlReader = XmlReader.Create(s);

            while(xmlReader.Read())
            {
                if (xmlReader.NodeType.Equals(XmlNodeType.Element))
                    switch(xmlReader.Name)
                    {
                        case "temp_f":
                            xmlReader.Read();
                            this.Temperature = decimal.Parse(xmlReader.Value);
                            break;
                        case "wind_mph":
                            xmlReader.Read();
                            this.WindSpeed = decimal.Parse(xmlReader.Value);
                            break;
                        case "wind_dir":
                            xmlReader.Read();
                            this.WindDirection = xmlReader.Value;
                            break;
                        case "weather":
                            xmlReader.Read();
                            this.Clouds = xmlReader.Value;
                            break;
                    }
            }
        }

        public decimal Temperature
        {
            get { return ImmediateWeather.Temperature; }
            set {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    immediateWeather.Temperature = value;
                    RaisePropertyChanged("Temperature");
                });
            }
        }

        public decimal WindSpeed
        {
            get { return ImmediateWeather.WindSpeed; }
            set {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    immediateWeather.WindSpeed = value;
                    RaisePropertyChanged("WindSpeed");
                });
            }
        }

        public string WindDirection
        {
            get { return ImmediateWeather.WindDirection; }
            set
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    immediateWeather.WindDirection = value;
                    RaisePropertyChanged("WindDirection");
                });
            }
        }

        public string Clouds
        {
            get { return ImmediateWeather.Clouds; }
            set {
                Deployment.Current.Dispatcher.BeginInvoke(()=> {
                immediateWeather.Clouds = value;
                RaisePropertyChanged("Clouds");
                });
            }
        }
    }
}
