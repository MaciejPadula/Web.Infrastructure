using Microsoft.Extensions.DependencyInjection;
using ServerTest.Contract.Interfaces;
using Web.Infrastructure.Microservices.Client.Exceptions;
using Web.Infrastructure.Microservices.Client.Extensions;

var services = new ServiceCollection();

services.AddMicroserviceClient<IUserService>("localhost:5051");

var provider = services.BuildServiceProvider();

var userService = provider.GetRequiredService<IUserService>();

try
{
    userService.AddUser(new() { UserName = "Maciej" });
    userService.AddUser(new() { UserName = "Test" });

    var value = userService.GetUsers();

    foreach (var u in value.Users)
    {
        Console.WriteLine(u.Name);
    }

    Console.ReadLine();
}
catch (MicroserviceResponseException ex)
{
    Console.WriteLine(ex.Message);
    Console.WriteLine((int)ex.StatusCode);
}

