// <copyright file="Clouds.cs" company="My Company Name">
// Copyright (c) 2022 All Rights Reserved
// </copyright>

using Newtonsoft.Json;

namespace WeatherApi
{
    public class Clouds
    {
        [JsonProperty("all")]
        public int All { get; set; }
    }
}
