using Web.Infrastructure.Cqrs.Mediator.Query;

namespace ExampleConsoleApp.User.GetUsers;

public class GetUsersQuery : IQuery<IEnumerable<User>>
{
    public IEnumerable<User> Result { get; set; } = default!;
}
