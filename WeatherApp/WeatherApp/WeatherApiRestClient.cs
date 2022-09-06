// <copyright file="WeatherApiRestClient.cs" company="My Company Name">
// Copyright (c) 2022 All Rights Reserved
// </copyright>

using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WeatherApi;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Windows;

namespace WeatherApp
{
    public class WeatherApiRestClient : IWeatherApiRestClient
    {
        private ILogger<WeatherApiRestClient> _logger;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private Uri SearchUriUriByCityName(string city, string apiKey) => new Uri($"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}");
        private Uri SearchUriUriByZipCode(string zip, string apiKey) => new Uri($"https://api.openweathermap.org/data/2.5/weather?zip={zip}&appid={apiKey}");
        private Uri SearchUriUriByCoordinates(double lat, double lon, string apiKey) => new Uri($"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={apiKey}");

        public WeatherApiRestClient(ILogger<WeatherApiRestClient> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _httpClient = new HttpClient();
        }

        public async Task<WeatherApiResponse> SearchWeatherByCityName(string city)
        {
            try
            {
                var cts = new CancellationTokenSource();
                cts.CancelAfter(TimeSpan.FromSeconds(Constants.TimeoutSeconds));
                var uriCity = Uri.EscapeUriString(city);
                var requestUri = SearchUriUriByCityName(uriCity, Constants.ApiKey);
                var response = await _httpClient.GetAsync(requestUri, cts.Token).ConfigureAwait(false);
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    MessageBox.Show("City [" + city + "] " + response.ReasonPhrase, "City Weather", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                _logger.LogInformation(content);
                var weather = JsonConvert.DeserializeObject<WeatherApiResponse>(content);
                return weather;
            }
            catch (Exception e)
            {
                var message = e.Message;
                _logger.LogError(e, message);
            }
            return new WeatherApiResponse { Cod = "500" };
        }

        public async Task<WeatherApiResponse> SearchWeatherByZipCode(string zip)
        {
            try
            {
                var cts = new CancellationTokenSource();
                cts.CancelAfter(TimeSpan.FromSeconds(Constants.TimeoutSeconds));
                var requestUri = SearchUriUriByZipCode(zip, Constants.ApiKey);
                var response = await _httpClient.GetAsync(requestUri, cts.Token).ConfigureAwait(false);
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    MessageBox.Show("Zip Code [" + zip + "] " + response.ReasonPhrase, "Zip Code Weather", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var weather = JsonConvert.DeserializeObject<WeatherApiResponse>(content);
                return weather;
            }
            catch (Exception e)
            {
                var message = e.Message;
                _logger.LogError(e, message);
            }
            return new WeatherApiResponse { Cod = "500" };
        }

        public async Task<WeatherApiResponse> SearchWeatherByCoordinates(double lat, double lon)
        {
            try
            {
                var cts = new CancellationTokenSource();
                cts.CancelAfter(TimeSpan.FromSeconds(Constants.TimeoutSeconds));
                var requestUri = SearchUriUriByCoordinates(lat, lon, Constants.ApiKey);
                var response = await _httpClient.GetAsync(requestUri, cts.Token).ConfigureAwait(false);
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    MessageBox.Show("The Weather of give coordinates " + response.ReasonPhrase, "City Weather", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var weather = JsonConvert.DeserializeObject<WeatherApiResponse>(content);
                return weather;
            }
            catch (Exception e)
            {
                var message = e.Message;
                _logger.LogError(e, message);
            }
            return new WeatherApiResponse { Cod = "500" };
        }
    }
}
