# Web.Infrastructure
This repository will contain my implementations of some basic web infrastructure patters.

# Web.Infrastructure.Microservices.Server
Provides extensions for Microsoft.DependencyInjection package that helps with registering your ASP.NET Controller as microservice interface. For example if you want to give clients contract that contains interface `IUsersService` you can create Controller that implements this and register it as Microservice:

`IUsersService.cs`
```C#
namespace Server.Contract;

public interface IUsersService
{
    void Register(string userName);
    List<string> GetUsers();
}
```

`UsersController.cs`
```C#
namespace Server;

public class UsersController : BaseController, IUsersService
{
    private readonly List<string> _users;

    public UsersController()
    {
        _users = new List<string>();
    }

    [HttpPost("Register/{userName}")]
    public void Register(string userName)
    {
        _users.Add(userName);
    }

    [HttpGet("GetUsers")]
    public List<string> GetUsers()
    {
        return _users;
    }
}
```

`Program.cs`
```C#
...
services.RegisterMicroservice<IUsersService, UsersController>();
...
```
Now your microservice is ready to use.

# Web.Infrastructure.Microservices.Client
## Basic usage
To use your microservice you need to add packages: `Server.Contract`, `Web.Infrastructure.Microservices.Client` to your project and configure `MicroserviceClient`:
`Program.cs`
```C#
services.AddMicroserviceClient<IUsersService>("http://localhost:5000", 
    builder => 
        builder.AddRequestMethodType("Register", HttpMethod.Post)
        builder.AddRequestMethodType("GetUsers", HttpMethod.Get)
);
```
You don't have to register endpoints. You can simply use this code:
```C#
services.AddMicroserviceClient<IUsersService>("http://localhost:5000");
```
but you have to be sure that all endpoints of your service are HttpPost. After this operaction you can get interface from ServiceProvider or use it with DependencyInjection:
<br/>
`Directly from ServiceProvider:`

```C#
using Microsoft.Extensions.DependencyInjection;
using ServerTest.Contract;
using Web.Infrastructure.Microservices.Client.Exceptions;
using Web.Infrastructure.Microservices.Client.Extensions;

var services = new ServiceCollection();

services.AddMicroserviceClient<IUsersService>("http://localhost:5000", 
    builder => 
        builder.AddRequestMethodType("Register", HttpMethod.Post)
        builder.AddRequestMethodType("GetUsers", HttpMethod.Get)
);

var provider = services.BuildServiceProvider();

var userService = provider.GetRequiredService<IUsersService>();

var users = userService.GetUsers(); // will call your api and return synchronous data
```

`Dependency Injection:`

```C#
namespace Client;

public class SomeController : BaseController
{
    private readonly IUsersService _userService

    public SomeController(IUsersService userService)
    {
        _userService = userService;
    }

    [HttpGet("GetUsersFromService")]
    public IActionResult GetUsersFromService()
    {
        var users = _userService.GetUsers();
        return Ok(users);
    }
}
```
#
## IServiceLookup interface
If you want to resolve services addresses automaticly you can create your own implementation of `IServiceLookup` interface and inject it as Scoped before adding microservice client:
```C#
services.AddScoped<IServiceLookup, YourServiceLookup>();
services.AddMicroserviceClient<IUsersService>("some_microservice_name_that_your_implementation_can_recognize");
```

Interface definition:
```C#
namespace Web.Infrastructure.Microservices.Client.Logic.ServiceLookup
{
    public interface IServiceLookup
    {
        Uri Lookup(string serviceName);
    }
}
```