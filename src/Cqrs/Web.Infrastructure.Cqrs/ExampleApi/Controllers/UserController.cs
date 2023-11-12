using ExampleApi.Models;
using ExampleApi.UsersList;
using ExampleApi.UsersRegistration;
using Microsoft.AspNetCore.Mvc;

namespace ExampleApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUsersListService _usersListService;
    private readonly IRegisterUserService _registerUserService;

    public UserController(IUsersListService usersListService, IRegisterUserService registerUserService)
    {
        _usersListService = usersListService;
        _registerUserService = registerUserService;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsersList()
    {
        return Ok(await _usersListService.GetAllUsers());
    }

    [HttpPost]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest request)
    {
        await _registerUserService.RegisterUser(request.Email, request.Name);
        return Ok();
    }
}
