// See https://aka.ms/new-console-template for more information
using CurrentWeatherCLIApp.Services;

Console.WriteLine("Console app which can get current weather in your city");
Console.WriteLine();

Console.Write("Input your city: ");
string city = Console.ReadLine().ToLower();
Console.WriteLine();

if (city.Length == 0 || city == null)
{
    Console.WriteLine("you didn't input valid city");
}
else
{
    Console.WriteLine();
    WeatherService wService = new WeatherService();

    wService.GetWeather(city);
}

Console.ReadLine();