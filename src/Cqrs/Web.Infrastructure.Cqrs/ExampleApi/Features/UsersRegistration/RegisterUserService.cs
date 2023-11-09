using ExampleApi.Models;
using Web.Infrastructure.Cqrs.Mediator;

namespace ExampleApi.Features.UsersRegistration;

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
        var validateEmailQuery = new ValidateEmailQuery
        {
            Email = email
        };
        _mediator.HandleQuery(validateEmailQuery);

        if (!validateEmailQuery.Result)
        {
            throw new Exception("Email is invalid!");
        }
        
        var findUserQuery = new FindUserQuery
        {
            Email = email
        };
        await _mediator.HandleQueryAsync(findUserQuery);

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
