using BLL.Abstractions;
using BLL.DTO;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("get-all")]
    public IActionResult GetAll()
    {
        List<UserDTO> users;
        try
        {
            users = _userService.GetAllUsers();
        }
        catch
        {
            users = new List<UserDTO>();
        }

        return new OkObjectResult(users);
    }

    [HttpGet("get-{id}")]
    public IActionResult Get(int id)
    {
        UserDTO? user = null;
        try
        {
            user = _userService.Get(id);
        }
        catch
        {
            return new NotFoundResult();
        }

        return new OkObjectResult(user);
    }

    [HttpPost("create")]
    public IActionResult Create(string username, string password, string email)
    {
        try
        {
            _userService.Create(username, password, email);
        }
        catch
        {
            return new BadRequestResult();
        }

        return new OkResult();
    }


    [HttpPut("update-{id}")]
    public IActionResult Update(int id, string? newName, string? newPassword, string? newEmail)
    {
        try
        {
            _userService.Update(id, newName, newPassword, newEmail);
        }
        catch
        {
            // todo: в репозитории выбрасывать правильные исключения в зависимости от ситуации
            // и обрабатыввать тут каждый экспешн, например, если реально в бд нет такого дневника,
            // то выдавать NotFoundResult, иначе BadRequestResult
            return new NotFoundResult();
        }

        return new OkResult();
    }

    [HttpDelete("delete-{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            _userService.Delete(id);
        }
        catch
        {
            return new NotFoundResult();
        }

        return new OkResult();
    }
}