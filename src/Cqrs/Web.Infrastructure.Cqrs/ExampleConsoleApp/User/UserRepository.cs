namespace ExampleConsoleApp.User;

internal class UserRepository
{
    private readonly IList<User> _users = new List<User>();

    public void AddUser(User user)
    {
        _users.Add(user);
    }

    public IEnumerable<User> GetUsers()
    {
        return _users;
    }
}
