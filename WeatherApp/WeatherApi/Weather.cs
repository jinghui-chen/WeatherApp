// <copyright file="Weather.cs" company="My Company Name">
// Copyright (c) 2022 All Rights Reserved
// </copyright>

using Newtonsoft.Json;

namespace WeatherApi
{
    public class Weather
    {
		[JsonProperty("id")]
		public int Id { get; set; }
		[JsonProperty("main")]
		public string Main { get; set; }
		[JsonProperty("description")]
		public string Description { get; set; }
		[JsonProperty("icon")]
		public string Icon { get; set; }
	}
}
