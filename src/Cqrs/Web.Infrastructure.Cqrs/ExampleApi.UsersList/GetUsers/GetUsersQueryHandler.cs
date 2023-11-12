using ExampleApi.Model;
using Web.Infrastructure.Cqrs.Mediator.Query;

namespace ExampleApi.UsersList.GetUsers;

internal class GetUsersQueryHandler : IAsyncQueryHandler<GetUsersQuery>
{
    private readonly IUsersRepository _usersRepository;

    public GetUsersQueryHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task HandleAsync(GetUsersQuery query)
    {
        query.Result = await _usersRepository.GetAll();
    }
}
