using ExampleApi.Model;
using ExampleApi.UsersRegistration.AddUser;
using ExampleApi.UsersRegistration.FindUser;
using ExampleApi.UsersRegistration.ValidateEmail;
using Web.Infrastructure.Cqrs.Mediator;

namespace ExampleApi.UsersRegistration;

public interface IRegisterUserService
{
    Task RegisterUser(string email, string name);
}

internal class RegisterUserService : IRegisterUserService
{
    private readonly IMediator _mediator;

    public RegisterUserService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task RegisterUser(string email, string name)
    {
        var validateEmailQuery = _mediator.HandleQuery(new ValidateEmailQuery
        {
            Email = email
        });

        if (!validateEmailQuery.Result)
        {
            throw new Exception("Email is invalid!");
        }

        var findUserQuery = await _mediator.HandleQueryAsync(new FindUserQuery
        {
            Email = email
        });

        if (findUserQuery.Result)
        {
            throw new Exception("User already exists!");
        }

        await _mediator.HandleCommandAsync(new AddUserCommand
        {
            NewUser = new User { Id = Guid.NewGuid(), Email = email, Name = name }
        });
    }
}
