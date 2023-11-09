using ExampleApi.Features.UsersList;
using ExampleApi.Features.UsersRegistration;
using ExampleApi.Repositories;
using Web.Infrastructure.Cqrs.Mediator.NetCoreExtensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<IUsersRepository, UsersRepository>();
builder.Services.AddUsersRegistration();
builder.Services.AddUsersList();
builder.Services.AddMediator();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();