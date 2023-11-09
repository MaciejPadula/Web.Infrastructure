using ExampleConsoleApp.User;
using ExampleConsoleApp.User.AddUser;
using ExampleConsoleApp.User.FilterUsers;
using ExampleConsoleApp.User.GetUsers;
using Microsoft.Extensions.DependencyInjection;
using Web.Infrastructure.Cqrs.Mediator;
using Web.Infrastructure.Cqrs.Mediator.Command;
using Web.Infrastructure.Cqrs.Mediator.NetCoreExtensions;
using Web.Infrastructure.Cqrs.Mediator.Query;

var services = new ServiceCollection();

services.AddSingleton<UserRepository>();

services.AddScoped<ICommandHandler<AddUserCommand>, AddUserCommandHandler>();
services.AddScoped<IQueryHandler<GetUsersQuery>, GetUsersQueryHandler>();
services.AddScoped<IQueryHandler<FilterUsersQuery>, FilterUsersQueryHandler>();
services.AddMediator();

var provider = services.BuildServiceProvider();
var mediator = provider.GetRequiredService<IMediator>();


mediator.HandleCommand(new AddUserCommand
{
    Value = new User { Id = 1, Name = "Test" }
});

mediator.HandleCommand(new AddUserCommand
{
    Value = new User { Id = 2, Name = "Maciej" }
});

var usersQuery = new GetUsersQuery();

mediator.HandleQuery(usersQuery);

foreach (var user in usersQuery.Result)
{
    Console.WriteLine(user);
}


var filterQuery = new FilterUsersQuery
{
    Name = "Mac"
};

mediator.HandleQuery(filterQuery);

foreach (var user in filterQuery.Result)
{
    Console.WriteLine(user);
}

