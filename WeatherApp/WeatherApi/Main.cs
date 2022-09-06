// <copyright file="Main.cs" company="My Company Name">
// Copyright (c) 2022 All Rights Reserved
// </copyright>

using Newtonsoft.Json;

namespace WeatherApi
{
    public class Main
    {
		[JsonProperty("temp")]
		public double Temp { get; set; }
		[JsonProperty("feels_like")]
		public double FeelsLike { get; set; }
		[JsonProperty("temp_min")]
		public double TempMin { get; set; }
		[JsonProperty("temp_max")]
		public double TempMax { get; set; }
		[JsonProperty("pressure")]
		public double Pressure { get; set; }
		[JsonProperty("humidity")]
		public double Humidity { get; set; }
		[JsonProperty("sea_level")]
		public double SeaLevel { get; set; }
		[JsonProperty("grnd_level")]
		public double GroundLevel { get; set; }
	}
}
