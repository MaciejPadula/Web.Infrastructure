using ExampleApi.Repositories;
using Web.Infrastructure.Cqrs.Mediator.Command;

namespace ExampleApi.Features.UsersRegistration;

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
