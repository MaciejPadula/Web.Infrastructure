using Web.Infrastructure.Cqrs.Mediator.Query;

namespace ExampleApi.UsersRegistration.FindUser;

internal class FindUserQuery : IQuery<bool>
{
    public required string Email { get; set; }
    public bool Result { get; set; }
}
