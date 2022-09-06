# WeatherApp
The app allows a user to search for current weather conditions by city name, zip code, or coordinates
by using the the https://openweathermap.org/api API

![alt text](https://github.com/jinghui-chen/WeatherApp/blob/main/Images/WeatherApp.png?raw=true)

### Application Technology
-- WPF dotnet core, C#, Microsoft.Extensions, Serilog

### How to Use
- Search by city name:  put focus after text box after city name, type a city, and click on buttion "Get Weather"
- Search by zip code:  put focus after text box after zip code, type in zip code, and click on buttion "Get Weather"
- Search by latitude/longitude:  put focus after text boxes after latitude/longitude, type in your coordinates, and click on buttion "Get Weather"

### UI Behaviors
When city name is selected as search criteria, all others will be cleared
When zip code is selected as search criteria, all others will be cleared
When coordinates are selected, city name and zip code will be cleared.

If given location is not available, a popup message box will show up.

### Weather related Data Types
- WeatherApiResponse
  * All fields - The API response object which hold all weather data
- Coordinates
  * Latitude - latitude of the location
  * Longitude - longitude of the location
- Wind
  * Speed -  meters per second
  * Degree - meteorological degree
  * Gust - meters per second
- Weather
  * Id - OpenWeatherMap API Id
  * Main - Brief description of the weather
  * Description - Description of current weather condiction
  * Icon - OpenWeatherMap icon Id
- Sys
  * Type - OpenWeatherMap Type
  * Id - OpenweatherMap API city Id
  * Country - Country code
  * Sunrise - Sunrise time in unix time.
  * Sunset - Sunset time in unix time.
- Clouds
  * All - Percentage of clouds
- Snow
  * H1 - Last 1 hour if it snows.
- Rain
  * H1 - Last 1 hours if it rains.
- Main
  * Temp - Kelvin values of current temperature
  * FeelsLike - Feels like Kelvin values of current temperature
  * TempMin - Min temperature in Kelvin
  * TempMax- Max temperature in Kelvin
  * Humidity - Percentage
  * Pressure - Atmospheric pressure, hPa
  * SeaLevel - Atmospheric pressure on sea level, hPa
  * GroundLevel - Atmospheric pressure on ground level, hPa
