using Web.Infrastructure.Cqrs.Mediator.Query;

namespace ExampleConsoleApp.User.FilterUsers;
internal class FilterUsersQuery : IQuery<IEnumerable<User>>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public IEnumerable<User> Result { get; set; } = default!;
}
