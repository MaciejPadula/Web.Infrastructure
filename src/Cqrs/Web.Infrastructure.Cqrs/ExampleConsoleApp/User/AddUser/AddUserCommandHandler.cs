using Web.Infrastructure.Cqrs.Mediator.Command;

namespace ExampleConsoleApp.User.AddUser;

internal class AddUserCommandHandler : ICommandHandler<AddUserCommand>
{
    private readonly UserRepository _userRepository;

    public AddUserCommandHandler(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void Handle(AddUserCommand command)
    {
        _userRepository.AddUser(command.Value);
    }
}
