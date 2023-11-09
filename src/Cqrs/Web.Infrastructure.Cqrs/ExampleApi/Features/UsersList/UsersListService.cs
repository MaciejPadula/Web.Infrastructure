using ExampleApi.Models;
using Web.Infrastructure.Cqrs.Mediator;

namespace ExampleApi.Features.UsersList;

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
        var query = new GetUsersQuery();
        await _mediator.HandleQueryAsync(query);
        return query.Result;
    }
}
