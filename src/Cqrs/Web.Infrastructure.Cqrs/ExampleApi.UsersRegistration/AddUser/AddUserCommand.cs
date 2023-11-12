using ExampleApi.Model;
using Web.Infrastructure.Cqrs.Mediator.Command;

namespace ExampleApi.UsersRegistration.AddUser;

internal class AddUserCommand : ICommand
{
    public required User NewUser { get; set; }
}
