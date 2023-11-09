using ExampleApi.Models;
using Web.Infrastructure.Cqrs.Mediator.Command;

namespace ExampleApi.Features.UsersRegistration;

internal class AddUserCommand : ICommand
{
    public required User NewUser { get; set; }
}
