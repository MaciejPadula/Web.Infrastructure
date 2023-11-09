using Web.Infrastructure.Cqrs.Mediator.Query;

namespace ExampleApi.Features.UsersRegistration;

internal class FindUserQuery : IQuery<bool>
{
    public required string Email { get; set; }
    public bool Result { get; set; }
}
