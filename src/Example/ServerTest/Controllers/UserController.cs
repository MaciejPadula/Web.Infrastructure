﻿using Microsoft.AspNetCore.Mvc;
using ServerTest.Contract.Interfaces;
using ServerTest.Contract.Models.Requests;
using ServerTest.Contract.Models.Responses;
using ServerTest.Repository;

namespace ServerTest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("AddUser")]
        public Task AddUser(AddUserRequest request)
        {
            _userRepository.Add(request.UserName);
            return Task.CompletedTask;
        }

        [HttpGet("GetUsers")]
        public Task<GetUsersResponse> GetUsers()
        {
            return Task.FromResult(new GetUsersResponse
            {
                Users = _userRepository.Get()
            });
        }
    }
}