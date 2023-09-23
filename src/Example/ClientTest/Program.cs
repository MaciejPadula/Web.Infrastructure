using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServerTest.Contract.Interfaces;
using Web.Infrastructure.Microservices.Client.Exceptions;
using Web.Infrastructure.Microservices.Client.Extensions;
using Web.Infrastructure.Microservices.Client.Configuration.Extensions;
using Web.Infrastructure.Microservices.Client.Builders;
using Microsoft.Extensions.Logging;

var config = new ConfigurationBuilder()
    .AddInMemoryCollection(new List<KeyValuePair<string, string?>>()
    {
        new("MSRV:ServerTest.Contract.Interfaces", "http://localhost:5051")
    })
    .Build();

var services = new ServiceCollection();
services.AddLogging(b => b.AddConsole());
services.AddSingleton<IConfiguration>(config);
services.AddConfigurationServiceLookup("MSRV");
services.AddMicroserviceClient<IUserService>(builder =>
{
    builder.AddRequestMethodType("GetUsers", HttpMethod.Get);
},
new MicroserviceClientConfigurationBuilder()
    .WithRequestTimeout(TimeSpan.FromSeconds(5))
    .WithRetries(3, TimeSpan.FromMilliseconds(500)));

var provider = services.BuildServiceProvider();

var userService = provider.GetRequiredService<IUserService>();

try
{
    await userService.AddUser(new() { UserName = "Maciej" });
    await userService.AddUser(new() { UserName = "Test" });

    var value = await userService.GetUsers();

    foreach (var u in value.Users)
    {
        Console.WriteLine(u.Name);
    }
}
catch (MicroserviceResponseException ex)
{
    Console.WriteLine(ex.Message);
    Console.WriteLine((int)ex.StatusCode);
}

Console.ReadLine();

