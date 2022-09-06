// <copyright file="MainWindow.cs" company="My Company Name">
// Copyright (c) 2022 All Rights Reserved
// </copyright>

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WeatherApi;

namespace WeatherApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly AppSettings _appSettings;
        private readonly IWeatherApiRestClient _apiRestClient;
        private readonly Func<double, double> KelvinToFahrenheit = (k) => { return (k - 273.15) * 1.8 + 32; };

        public MainWindow(IOptions<AppSettings> settings, IWeatherApiRestClient apiRestClient)
        {
            _appSettings = settings.Value;
            _apiRestClient = apiRestClient;
            InitializeComponent();
        }

        private async void buttonGetWeather_Click(object sender, RoutedEventArgs e)
        {
            ClearDisplayFields();

            var todayWeather = new WeatherApiResponse();
            if (!string.IsNullOrEmpty(textCityName.Text))
            {
                todayWeather = await _apiRestClient.SearchWeatherByCityName(textCityName.Text);
            }
            else if (!string.IsNullOrEmpty(textZipCode.Text))
            {
                todayWeather = await _apiRestClient.SearchWeatherByZipCode(textZipCode.Text);
            }
            else if (!string.IsNullOrEmpty(textLatitude.Text) && !(string.IsNullOrEmpty(textLongitude.Text)))
            {
                var lat = Convert.ToDouble(textLatitude.Text);
                var lon = Convert.ToDouble(textLongitude.Text);
                todayWeather = await _apiRestClient.SearchWeatherByCoordinates(lat, lon);
            }
            else
            {
                MessageBox.Show("Please provider City, or Zip Code, or Latitude and Longitude.", "Weather", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            /**/
            /**/
            if (!string.IsNullOrEmpty(todayWeather.Name))
            {
                textBlockCityName.Text = todayWeather.Name;
            }
            if (todayWeather.Coord != null)
            {
                textBlockLatitude.Text = todayWeather.Coord.Latitude.ToString("N3");
                textBlockLongitude.Text = todayWeather.Coord.Longitude.ToString("N3");
            }
            if (todayWeather.Weather != null)
            {
                textBlockDescription.Text = todayWeather.Weather.First().Description;
            }
            if (todayWeather.Main != null)
            {
                textBlockHumidity.Text = todayWeather.Main.Humidity.ToString();
                textBlockTemp.Text = KelvinToFahrenheit(todayWeather.Main.Temp).ToString("N3");
            }
            if (todayWeather.Cod != "500")
            {
                var json = JsonConvert.SerializeObject(todayWeather, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                textBoxJson.Text = json;
            }
        }

        private void ClearDisplayFields()
        {
            textBlockCityName.Text = "";
            textBlockLatitude.Text = "";
            textBlockLongitude.Text = "";
            textBlockDescription.Text = "";
            textBlockHumidity.Text = "";
            textBlockTemp.Text = "";
            textBoxJson.Text = "";
        }

        private void buttonExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void textCityName_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            textZipCode.Text = "";
            textLatitude.Text = "";
            textLongitude.Text = "";

        }

        private void textZipCode_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            textCityName.Text = "";
            textLatitude.Text = "";
            textLongitude.Text = "";
        }

        private void textLatitude_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            textCityName.Text = "";
            textZipCode.Text = "";
        }

        private void textLongitude_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            textCityName.Text = "";
            textZipCode.Text = "";
        }
    }
}
