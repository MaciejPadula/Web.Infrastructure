using ExampleApi.Model;
using ExampleApi.UsersList.GetUsers;
using Web.Infrastructure.Cqrs.Mediator;

namespace ExampleApi.UsersList;

public interface IUsersListService
{
    Task<IEnumerable<User>> GetAllUsers();
}

internal class UsersListService : IUsersListService
{
    private readonly IMediator _mediator;

    public UsersListService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        var query = await _mediator.HandleQueryAsync(new GetUsersQuery());
        return query.Result;
    }
}
