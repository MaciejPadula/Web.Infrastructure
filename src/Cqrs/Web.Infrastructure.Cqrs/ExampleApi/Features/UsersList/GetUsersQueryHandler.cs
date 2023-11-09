using ExampleApi.Repositories;
using Web.Infrastructure.Cqrs.Mediator.Query;

namespace ExampleApi.Features.UsersList;

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
