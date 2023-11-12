using Web.Infrastructure.Cqrs.Mediator.Command;

namespace ExampleConsoleApp.User.AddUser;

internal class AddUserCommand : ICommand
{
    public User Value { get; set; }
}
