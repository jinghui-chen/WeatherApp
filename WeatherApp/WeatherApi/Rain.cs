// <copyright file="Rain.cs" company="My Company Name">
// Copyright (c) 2022 All Rights Reserved
// </copyright>

using Newtonsoft.Json;

namespace WeatherApi
{
    public class Rain
    {
        [JsonProperty("1h")]
        public double H1 { get; set; }
    }
}
