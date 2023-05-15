using Microsoft.Extensions.DependencyInjection;
using ServerTest.Contract.Interfaces;
using Web.Infrastructure.Microservices.Client.Exceptions;
using Web.Infrastructure.Microservices.Client.Extensions;

var services = new ServiceCollection();

services.AddMicroserviceClient<IUserService>("localhost:5051", builder =>
{
    builder.AddRequestMethodType("GetUsers", HttpMethod.Get);
});

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

