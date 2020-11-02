using EasyInterests.API.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EasyInterests.API.Controllers
{
  [ApiController]
  [Route("api/v1/users")]
  public class UserController : ControllerBase
  {
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
      _userService = userService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
      return NoContent();
    }
  }
}
