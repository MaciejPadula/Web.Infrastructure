namespace ServerTest.Contract
{
    public class WeatherForecastGetResponse
    {
        public IEnumerable<WeatherForecast>? Forecasts { get; set; }
    }
}
