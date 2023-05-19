using Microsoft.AspNetCore.Mvc;
using ServerTest.Contract.Interfaces;
using ServerTest.Contract.Models.Requests;
using ServerTest.Contract.Models.Responses;
using ServerTest.Repository;
using Web.Infrastructure.Microservices.Server;

namespace ServerTest.Controllers
{
    [MicroserviceController]
    public class UserController : ControllerBase, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        public Task AddUser([FromBody] AddUserRequest request)
        {
            _userRepository.Add(request.UserName);
            return Task.CompletedTask;
        }

        [HttpGet]
        public Task<GetUsersResponse> GetUsers()
        {
            return Task.FromResult(new GetUsersResponse
            {
                Users = _userRepository.Get()
            });
        }
    }
}
