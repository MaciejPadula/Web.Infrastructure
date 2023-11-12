using Web.Infrastructure.Cqrs.Mediator.Query;

namespace ExampleApi.UsersRegistration.ValidateEmail;

internal class ValidateEmailQuery : IQuery<bool>
{
    public required string Email { get; set; }
    public bool Result { get; set; }
}
