using ServerTest.Contract.Models.Requests;
using ServerTest.Contract.Models.Responses;

namespace ServerTest.Contract.Interfaces
{
    public interface IUserService
    {
        void AddUser(AddUserRequest request);
        GetUsersResponse GetUsers();
    }
}
