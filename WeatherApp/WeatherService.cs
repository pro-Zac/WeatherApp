using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace Weather.Cli;

public static class WeatherService
{
    const string API_KEY = "0db12240b44e35cf97411843a0ccdfe5";
    public static async Task Run(string[] args)
    {
        var httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://api.openweathermap.org/")
        };
        
        var continuesCheck = true;
        do
        {
            var city = args.AsQueryable().FirstOrDefault();
            if (city == null)
            {
                Console.Write("Enter the city: ");
                city = Console.ReadLine()!.Trim();
            }

            var response = await httpClient.GetAsync($"data/2.5/weather?q={city}&appid={API_KEY}&units=metric");
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                Console.WriteLine($"Weather not found for this city: {city}");
                continue;
            }

            var currentWeather = await response.Content.ReadFromJsonAsync<WeatherObject>();

            Console.WriteLine($"Current weather for city: {city} ");
            
            if (currentWeather != null)
            {
                Console.WriteLine($"City: {currentWeather.Name}");
                Console.WriteLine($"Country: {currentWeather.Sys?.Country}");
                Console.WriteLine($"Weather: {currentWeather.Weather![0].Description}");
                Console.WriteLine($"Temperature: {currentWeather.Main!.Temp}°C");
                Console.WriteLine($"Humidity: {currentWeather.Main!.Humidity}%");
                Console.WriteLine($"Pressure: {currentWeather.Main!.Pressure}hPa");
                Console.WriteLine($"Wind: {currentWeather.Wind!.Speed}m/s, {currentWeather!.Wind!.Deg}°");
                Console.WriteLine($"Clouds: {currentWeather.Clouds!.All}%");
            }
            
            Console.WriteLine($"Continues to check the weather for another city? (Y/N)");
            
            if (Console.ReadLine()!.Trim() != "Y")  continuesCheck = false;
            
        } while (continuesCheck);
        
    }
}

public class Coord  
{  
    [JsonPropertyName("lon")]
    public double Lon { get; set; }  
    [JsonPropertyName("lat")]
    public double Lat { get; set; }  
}  
  
public class Weather  
{  
    [JsonPropertyName("id")]
    public int Id { get; set; }  
    [JsonPropertyName("main")]
    public string? Main { get; set; }  
    [JsonPropertyName("description")]
    public string? Description { get; set; }  
    [JsonPropertyName("icon")]
    public string? Icon { get; set; }  
}  
  
public class Main  
{  
    [JsonPropertyName("temp")]
    public double Temp { get; set; }  
    [JsonPropertyName("feels_like")]
    public double FeelsLike { get; set; }  
    [JsonPropertyName("temp_min")]
    public double TempMin { get; set; }  
    [JsonPropertyName("temp_max")]
    public double TempMax { get; set; }  
    [JsonPropertyName("pressure")]
    public double Pressure { get; set; }  
    [JsonPropertyName("humidity")]
    public double Humidity { get; set; }  
}  
  
public class Wind  
{  
    [JsonPropertyName("speed")]
    public double Speed { get; set; }  
    [JsonPropertyName("deg")]
    public int Deg { get; set; }  
}  
    
public class Clouds  
{  
    [JsonPropertyName("all")]
    public int All { get; set; }  
}  
  
public class Sys  
{  
    [JsonPropertyName("type")]
    public int Type { get; set; }  
    [JsonPropertyName("id")]
    public int Id { get; set; }  
    [JsonPropertyName("country")]
    public string? Country { get; set; }  
    [JsonPropertyName("sunrise")]
    public int Sunrise { get; set; }  
    [JsonPropertyName("sunset")]
    public int Sunset { get; set; }  
}  
  
public class WeatherObject  
{  
    [JsonPropertyName("coord")]
    public Coord? Coord { get; set; }  
    [JsonPropertyName("weather")]
    public List<Weather>? Weather { get; set; }  
    public string? @base { get; set; }  
    [JsonPropertyName("main")]
    public Main? Main { get; set; }  
    [JsonPropertyName("visibility")]
    public int? Visibility { get; set; }  
    [JsonPropertyName("wind")]
    public Wind? Wind { get; set; }
    [JsonPropertyName("clouds")]
    public Clouds? Clouds { get; set; }  
    [JsonPropertyName("dt")]
    public int Dt { get; set; }  
    [JsonPropertyName("sys")]
    public Sys? Sys { get; set; }  
    [JsonPropertyName("timezone")]
    public int Timezone { get; set; }  
    [JsonPropertyName("id")]
    public int Id { get; set; }  
    [JsonPropertyName("name")]
    public string? Name { get; set; }  
    [JsonPropertyName("cod")]
    public int Cod { get; set; }  
}  