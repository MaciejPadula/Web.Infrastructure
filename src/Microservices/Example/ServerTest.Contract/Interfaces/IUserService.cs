using ServerTest.Contract.Models.Requests;
using ServerTest.Contract.Models.Responses;

namespace ServerTest.Contract.Interfaces
{
    public interface IUserService
    {
        Task AddUser(AddUserRequest request);
        Task<GetUsersResponse> GetUsers();
    }
}
