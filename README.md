# Web.Infrastructure
This repository will contain my implementations of some basic web infrastructure patters.

# Web.Infrastructure.Microservices.Server
Provides extensions for Microsoft.DependencyInjection package that helps with registering your ASP.NET Controller as microservice interface. For example if you want to give clients contract that contains interface `IUsersService` you can create Controller that implements this and register it as Microservice:

`IUserService.cs`
```C#
namespace Server.Contract;

public interface IUserService
{
    Task AddUser(AddUserRequest request);
    Task<GetUsersResponse> GetUsers();
}
```

`UserController.cs`
```C#
namespace Server;

public class UserController : ControllerBase, IUserService
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost]
    public Task AddUser([FromBody] AddUserRequest request)
    {
        _userRepository.Add(request.UserName);
        return Task.CompletedTask;
    }

    [HttpGet]
    public Task<GetUsersResponse> GetUsers()
    {
        return Task.FromResult(new GetUsersResponse
        {
            Users = _userRepository.Get()
        });
    }
}
```

`Program.cs`
```C#
builder.Services.AddMicroserviceEndpointResolver();
...
var app = builder.Build();
...
app.RegisterMicroservice<IUserService, UserController>();
app.Run();
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

var users = await userService.GetUsers(); // will call your api and return asynchronous data
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
    public async Task<IActionResult> GetUsersFromService()
    {
        var users = await _userService.GetUsers();
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
There are two basic implementations ready to use: \
*Default*
```C#
namespace Web.Infrastructure.Microservices.Client.Logic
{
    internal class DefaultServiceLookup : IServiceLookup
    {
        public Uri Lookup(string serviceName)
        {
            return new Uri($"{serviceName}");
        }
    }
}
```
`Configuration`
```C#
namespace Web.Infrastructure.Microservices.Client.Configuration.Logic
{
    public class ConfigurationServiceLookup : IServiceLookup
    {
        private readonly IConfiguration _configuration;
        private readonly string _microservicesParentPrefix;

        public ConfigurationServiceLookup(
            IConfiguration configuration,
            string microservicesParentPrefix)
        {
            _configuration = configuration;
            _microservicesParentPrefix = microservicesParentPrefix;
        }

        public Uri Lookup(string serviceName)
        {
            return new Uri(_configuration[$"{_microservicesParentPrefix}:{serviceName}"] 
                ?? string.Empty);
        }
    }
}
```
