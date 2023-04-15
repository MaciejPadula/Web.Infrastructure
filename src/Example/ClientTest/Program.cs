using Microsoft.Extensions.DependencyInjection;
using ServerTest.Contract;
using Web.Infrastructure.Microservices.Client.Exceptions;
using Web.Infrastructure.Microservices.Client.Extensions;

var services = new ServiceCollection();

services.AddMicroserviceClient<IWeatherForecastService>("localhost:5000", 
    builder => 
        builder.AddRequestMethodType("Get", HttpMethod.Get)
);

var provider = services.BuildServiceProvider();

var weatherForecastService = provider.GetRequiredService<IWeatherForecastService>();

try
{
    var value = weatherForecastService.Get();

    foreach (var v in value.Forecasts)
    {
        Console.WriteLine(v.Summary);
    }
}
catch(MicroserviceResponseException ex)
{
    Console.WriteLine(ex.Message);
    Console.WriteLine((int)ex.StatusCode);
}

