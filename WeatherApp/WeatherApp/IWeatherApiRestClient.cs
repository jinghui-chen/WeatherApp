// <copyright file="IWeatherApiRestClient.cs" company="My Company Name">
// Copyright (c) 2022 All Rights Reserved
// </copyright>

using System.Threading.Tasks;
using WeatherApi;

namespace WeatherApp
{
    public interface IWeatherApiRestClient
    {
        Task<WeatherApiResponse> SearchWeatherByCityName(string city);
        Task<WeatherApiResponse> SearchWeatherByZipCode(string zip);
        Task<WeatherApiResponse> SearchWeatherByCoordinates(double lat, double lon);
    }
}
