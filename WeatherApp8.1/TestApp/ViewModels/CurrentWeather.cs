using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.ViewModel;
using System.Net;
using TestApp.Model;
using System.IO;
using System.Threading;
using System.Xml;
using System.ComponentModel;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Data;

namespace TestApp.ViewModels
{
    class CurrentWeather : NotificationObject
    {
        private string currentLocation;
        private WebRequest request;
        private Weather immediateWeather = new Weather();

        public CurrentWeather(string location)
        {
            currentLocation = location;
            request = WebRequest.Create(string.Format("http://api.worldweatheronline.com/free/v1/weather.ashx?q={0}&key=5ay9y7urmtdcm2v82p34qbzg&num_of_days=1", currentLocation));
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
            XmlReader xmlReader = XmlReader.Create(s);
            while(xmlReader.Read())
            {
                if (xmlReader.NodeType.Equals(XmlNodeType.EndElement) && xmlReader.Name.Equals("current_condition"))
                    return;
                else if (xmlReader.NodeType.Equals(XmlNodeType.Element))
                    switch (xmlReader.Name.ToLower())
                    {
                        case "temp_f":
                            xmlReader.Read();
                            immediateWeather.Temperature = decimal.Parse(xmlReader.Value);
                            break;
                        case "windspeedmiles":
                            xmlReader.Read();
                            immediateWeather.WindSpeed = decimal.Parse(xmlReader.Value);
                            break;
                        case "winddir16point":
                            xmlReader.Read();
                            immediateWeather.WindDirection = xmlReader.Value;
                            break;
                        case "weatherdesc":
                            xmlReader.Read();
                            immediateWeather.Clouds = xmlReader.Value;
                            break;
                    }
            }
        }

        public string CurrentLocation
        {
            get { return currentLocation; }
            set
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    currentLocation = value;
                    RaisePropertyChanged("CurrentLocation");
                    //GetWeather();
                }
                );
            }
        }

        public decimal Temperature
        {
            get { return immediateWeather.Temperature; }
            set {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    immediateWeather.Temperature = value;
                    RaisePropertyChanged("Temperature");
                }
                );
            }
        }

        public decimal WindSpeed
        {
            get { return immediateWeather.WindSpeed; }
            set {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    immediateWeather.WindSpeed = value;
                    RaisePropertyChanged("WindSpeed");
                }
                );
            }
        }

        public string WindDirection
        {
            get { return immediateWeather.WindDirection; }
            set
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    immediateWeather.WindDirection = value;
                    RaisePropertyChanged("WindSpeed");
                }
                );
            }
        }

        public string Clouds
        {
            get { return immediateWeather.Clouds; }
            set {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    immediateWeather.Clouds = value;
                    RaisePropertyChanged("Clouds");
                }
                );
            }
        }
    }
}
