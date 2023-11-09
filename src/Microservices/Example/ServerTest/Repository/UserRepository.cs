using ServerTest.Contract.Models;

namespace ServerTest.Repository
{
    public interface IUserRepository
    {
        int Add(string userName);
        List<User> Get();
    }

    public class UserRepository : IUserRepository
    {
        private readonly DbSet<User> _users = new();

        public int Add(string userName)
        {
           return _users.Add(new User { Name = userName }, (key, user) => user.Id = key);
        }

        public List<User> Get()
        {
            return _users.Get();
        }
    }
}
