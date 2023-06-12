namespace API
{
    public interface IWeatherForecastService
    {
        IEnumerable<WeatherForecast> Get(int number, int minTemp, int maxTemp);
    }
}