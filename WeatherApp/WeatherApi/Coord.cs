// <copyright file="Coord.cs" company="My Company Name">
// Copyright (c) 2022 All Rights Reserved
// </copyright>

using Newtonsoft.Json;

namespace WeatherApi
{
    public class Coord
    {
        [JsonProperty("lat")]
        public double Latitude { get; set; }
        [JsonProperty("lon")]
        public double Longitude { get; set; }
    }
}
