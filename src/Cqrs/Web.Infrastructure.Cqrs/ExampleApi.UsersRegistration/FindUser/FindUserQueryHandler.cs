using ExampleApi.Model;
using Web.Infrastructure.Cqrs.Mediator.Query;

namespace ExampleApi.UsersRegistration.FindUser;

internal class FindUserQueryHandler : IAsyncQueryHandler<FindUserQuery>
{
    private readonly IUsersRepository _usersRepository;

    public FindUserQueryHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task HandleAsync(FindUserQuery query)
    {
        var users = await _usersRepository.GetAll();

        query.Result = users.Any(u => u.Email == query.Email);
    }
}
