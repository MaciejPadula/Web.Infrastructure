using Web.Infrastructure.Cqrs.Mediator.Query;

namespace ExampleConsoleApp.User.GetUsers;
internal class GetUsersQueryHandler : IQueryHandler<GetUsersQuery>
{
    private readonly UserRepository _userRepository;

    public GetUsersQueryHandler(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void Handle(GetUsersQuery query)
    {
        query.Result = _userRepository.GetUsers();
    }
}
