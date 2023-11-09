namespace ExampleApi.Models;

public class RegisterUserRequest
{
    public required string Email { get; set; }
    public required string Name { get; set; }
}
