using ExampleApi.Model;
using Web.Infrastructure.Cqrs.Mediator.Query;

namespace ExampleApi.UsersList.GetUsers;

internal class GetUsersQuery : IQuery<IEnumerable<User>>
{
    public IEnumerable<User> Result { get; set; } = default!;
}
