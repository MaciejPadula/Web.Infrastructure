using Web.Infrastructure.Cqrs.Mediator.Query;

namespace ExampleConsoleApp.User.FilterUsers;

internal class FilterUsersQueryHandler : IQueryHandler<FilterUsersQuery>
{
    private readonly UserRepository _userRepository;

    public FilterUsersQueryHandler(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void Handle(FilterUsersQuery query)
    {
        query.Result = _userRepository.GetUsers()
            .Where(u => u.Name.Contains(query.Name));
    }
}
