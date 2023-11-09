using ExampleApi.Models;
using Web.Infrastructure.Cqrs.Mediator.Query;

namespace ExampleApi.Features.UsersList;

internal class GetUsersQuery : IQuery<IEnumerable<User>>
{
    public IEnumerable<User> Result { get; set; } = default!;
}
