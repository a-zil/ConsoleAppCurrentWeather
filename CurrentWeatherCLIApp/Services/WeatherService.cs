using System.Text.Json;
using System.Text.Json.Nodes;

namespace CurrentWeatherCLIApp.Services
{
    public class WeatherService
    {
        private readonly string _apiKey = "4b781be9a7a4811bf89d75d18a175c33";

        public async void GetWeather(string city)
        {
            var url = $"http://api.openweathermap.org/data/2.5/weather?q={city}&appid={_apiKey}";

            HttpClient httpClient = new HttpClient();

            try
            {
                var responce = await httpClient.GetAsync(url);
                var responceBody = await responce.Content.ReadAsStringAsync();

                DeserializeResponceJSON(responceBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException caught!");
                Console.WriteLine($"Message: {e.Message}");
            }
        }

        private void DeserializeResponceJSON(string jsonString)
        {
            Console.WriteLine("Result     ========== ========== ========== ========== ========== ==========");
            Console.WriteLine();
            JsonNode weatherNode = JsonNode.Parse(jsonString);

            JsonNode cityName = weatherNode["name"];
            Console.WriteLine($"City: {cityName}");

            JsonNode weatherDescription = weatherNode["weather"][0]["description"];
            Console.WriteLine($"Current weather statement: {weatherDescription.ToJsonString()}");

            JsonNode temp = weatherNode["main"]["temp"];
            JsonNode feelsLike = weatherNode["main"]["feels_like"];
            double cTemp = ConvertKelvinToCelsius((double) temp);
            double cFeelsLike = ConvertKelvinToCelsius((double) feelsLike);
            Console.WriteLine($"Current temperature in K: {temp.ToJsonString()}");
            Console.WriteLine($"Current temperature in C: {cTemp}");
            Console.WriteLine($"Feels like in K: {feelsLike.ToJsonString()}");
            Console.WriteLine($"Feels like in C: {cFeelsLike}");

        }

        private double ConvertKelvinToCelsius(double tempK)
        {
            return Math.Round(tempK - 273.15); 
        }
    }
}
