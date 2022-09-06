// <copyright file="Wind.cs" company="My Company Name">
// Copyright (c) 2022 All Rights Reserved
// </copyright>

using Newtonsoft.Json;

namespace WeatherApi
{
    public class Wind
    {
        [JsonProperty("speed")]
        public double Speed { get; set; }
        [JsonProperty("deg")]
        public int Degree { get; set; }
        [JsonProperty("gust")]
        public double Gust { get; set; }
    }
}
