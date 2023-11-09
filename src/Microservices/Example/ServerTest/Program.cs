using ServerTest.Contract.Interfaces;
using ServerTest.Controllers;
using ServerTest.Repository;
using Web.Infrastructure.Microservices.Server.Builders;

var builder = new MicroserviceBuilder(args);

builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.RegisterMicroservice<IUserService, UserController>();

builder.ConfigureApp(app =>
{
    app.UseHttpsRedirection();
    app.UseAuthorization();
});

var app = builder.Build();
app.Run();
