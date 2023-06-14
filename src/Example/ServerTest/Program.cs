using ServerTest.Contract.Interfaces;
using ServerTest.Controllers;
using ServerTest.Repository;
using Web.Infrastructure.Microservices.Server.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMicroserviceEndpointResolver();
builder.Services.AddControllers();
builder.Services.AddSingleton<IUserRepository, UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseAuthorization();

app.RegisterMicroservice<IUserService, UserController>();

app.Run();
