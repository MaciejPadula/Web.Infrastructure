using ExampleApi.Model;
using Web.Infrastructure.Cqrs.Mediator.Command;

namespace ExampleApi.UsersRegistration.AddUser;

internal class AddUserCommandHandler : IAsyncCommandHandler<AddUserCommand>
{
    private readonly IUsersRepository _usersRepository;

    public AddUserCommandHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task HandleAsync(AddUserCommand command)
    {
        await _usersRepository.Add(command.NewUser);
    }
}
