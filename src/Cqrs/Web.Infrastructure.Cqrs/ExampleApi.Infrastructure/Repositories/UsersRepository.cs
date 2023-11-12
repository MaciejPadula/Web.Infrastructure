using ExampleApi.Model;

namespace ExampleApi.Infrastructure.Repositories;

internal class UsersRepository : IUsersRepository
{
    private readonly IList<User> _users = new List<User>();

    public Task Add(User entity)
    {
        _users.Add(entity);
        return Task.CompletedTask;
    }

    public Task<IEnumerable<User>> GetAll()
    {
        return Task.FromResult(_users.AsEnumerable());
    }

    public Task Update(User entity)
    {
        var existingUser = _users.First(u => u.Id == entity.Id);
        existingUser.Name = entity.Name;
        existingUser.Email = entity.Email;
        return Task.CompletedTask;
    }
}
