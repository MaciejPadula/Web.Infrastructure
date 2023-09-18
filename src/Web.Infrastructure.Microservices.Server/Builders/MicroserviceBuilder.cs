using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Web.Infrastructure.Microservices.Server.Extensions;
using Web.Infrastructure.Microservices.Server.Middleware;
using Web.Infrastructure.Microservices.Shared.Extensions;

namespace Web.Infrastructure.Microservices.Server.Builders;

public class MicroserviceBuilder
{
    private readonly WebApplicationBuilder _builder;
    private readonly List<Action<IEndpointRouteBuilder>> _registeredMicroservices = new();
    private readonly List<Action<WebApplication>> _webAppPredicates = new();

    public MicroserviceBuilder(string[] args)
    {
        _builder = WebApplication.CreateBuilder(args);
    }

    public IConfiguration Configuration => _builder.Configuration;
    public IServiceCollection Services => _builder.Services;

    public MicroserviceBuilder RegisterMicroservice<TService, TController>()
        where TController : ControllerBase, TService
    {
        _registeredMicroservices.Add(builder =>
        {
            builder.RegisterMicroservice<TService, TController>();
        });

        return this;
    }

    public MicroserviceBuilder ConfigureApp(Action<WebApplication> appPredicate)
    {
        _webAppPredicates.Add(appPredicate);

        return this;
    }

    public WebApplication Build()
    {
        Services.AddMicroserviceEndpointResolver();
        Services.AddControllers();
        Services.AddScoped<ExceptionsHandlingMiddleware>();

        var app = _builder.Build();
        _webAppPredicates.ForEach(webAppConfiguration => webAppConfiguration(app));
        _registeredMicroservices.ForEach(microservice => microservice(app));
        app.UseMiddleware<ExceptionsHandlingMiddleware>();
        return app;
    }
}
